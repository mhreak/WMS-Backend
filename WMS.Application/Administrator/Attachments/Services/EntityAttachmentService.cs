using Microsoft.Extensions.Configuration;
using WMS.Application.Administrator.Attachments.DTOs;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.File.Services;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Attachments;

namespace WMS.Application.Administrator.Attachments.Services;

public class EntityAttachmentService : IEntityAttachmentService
{
    private readonly IEntityAttachmentRepository _repo;
    private readonly IAttachmentTypeRepository _typeRepo;
    private readonly IImageThumbnailService _thumbnailService;
    private readonly IUnitOfWork _uow;
    private readonly string _rootPath;

    public EntityAttachmentService(
        IEntityAttachmentRepository repo,
        IAttachmentTypeRepository typeRepo,
        IImageThumbnailService thumbnailService,
        IUnitOfWork uow,
        IConfiguration configuration)
    {
        _repo = repo;
        _typeRepo = typeRepo;
        _thumbnailService = thumbnailService;
        _uow = uow;
        _rootPath = configuration["Storage:RootPath"] ?? "uploads";
    }

    public async Task<EntityAttachmentDto> UploadAsync(
        UploadEntityAttachmentRequest request,
        Guid currentUserId,
        CancellationToken ct = default)
    {
        if (request.File is null || request.File.Length == 0)
            throw new BadRequestException(MessageKeys.FileInvalid);

        var attachmentType = await _typeRepo.GetByIdAsync(request.AttachmentTypeId, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentTypeNotFound);

        if (!attachmentType.IsActive)
            throw new BadRequestException(MessageKeys.AttachmentTypeInactive);

        var originalExtension = "." + Path.GetExtension(request.File.FileName).Trim('.').ToLowerInvariant();
        if (originalExtension == ".")
            throw new BadRequestException(MessageKeys.FileInvalid);

        var entityFolder = Path.Combine(GetRootPath(), request.EntityId.ToString("N"));
        Directory.CreateDirectory(entityFolder);

        string storedFileName;
        string finalExtension;
        string? thumbnailFileName = null;
        long finalSize;

        if (_thumbnailService.IsConvertibleToWebp(originalExtension))
        {
            // تبدیل به webp و ذخیره
            var baseName = Guid.NewGuid().ToString("N");
            var destinationWithoutExt = Path.Combine(entityFolder, baseName);

            await using (var stream = request.File.OpenReadStream())
            {
                var savedPath = await _thumbnailService.SaveAsWebpAsync(stream, destinationWithoutExt, ct);
                storedFileName = Path.GetFileName(savedPath);
            }
            finalExtension = "webp";
            finalSize = new FileInfo(Path.Combine(entityFolder, storedFileName)).Length;

            thumbnailFileName = $"{baseName}_thumb.webp";
            var thumbnailFullPath = Path.Combine(entityFolder, "thumbs", thumbnailFileName);
            await _thumbnailService.GenerateThumbnailAsync(
                Path.Combine(entityFolder, storedFileName), thumbnailFullPath, targetWidth: 800, ct);
        }
        else if (_thumbnailService.IsImage(originalExtension)) // gif و مشابه، بدون تبدیل ولی با thumbnail
        {
            storedFileName = $"{Guid.NewGuid():N}{originalExtension}";
            finalExtension = originalExtension.TrimStart('.');
            var fullPath = Path.Combine(entityFolder, storedFileName);

            await using (var stream = System.IO.File.Create(fullPath))
                await request.File.CopyToAsync(stream, ct);

            finalSize = new FileInfo(fullPath).Length;

            thumbnailFileName = $"{Path.GetFileNameWithoutExtension(storedFileName)}_thumb.webp";
            var thumbnailFullPath = Path.Combine(entityFolder, "thumbs", thumbnailFileName);
            await _thumbnailService.GenerateThumbnailAsync(fullPath, thumbnailFullPath, targetWidth: 800, ct);
        }
        else
        {
            // فایل غیرعکسی (PDF, Word, ...) — بدون تغییر، بدون thumbnail
            storedFileName = $"{Guid.NewGuid():N}{originalExtension}";
            finalExtension = originalExtension.TrimStart('.');
            var fullPath = Path.Combine(entityFolder, storedFileName);

            await using (var stream = System.IO.File.Create(fullPath))
                await request.File.CopyToAsync(stream, ct);

            finalSize = new FileInfo(fullPath).Length;
        }

        var entity = new EntityAttachment
        {
            Id = Guid.NewGuid(),
            UserId = currentUserId,
            EntityId = request.EntityId,
            AttachmentTypeId = request.AttachmentTypeId,
            FileName = storedFileName,
            Extension = finalExtension,
            Size = finalSize,
            ThumbnailFileName = thumbnailFileName,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity, attachmentType.Title);
    }

    public async Task<List<EntityAttachmentDto>> GetByEntityIdAsync(
        Guid entityId,
        Guid? attachmentTypeId = null,
        CancellationToken ct = default)
    {
        var list = await _repo.GetByEntityIdAsync(entityId, attachmentTypeId, ct);
        return list.Select(x => MapToDto(x, x.AttachmentType?.Title ?? string.Empty)).ToList();
    }

    public async Task<EntityAttachmentDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentNotFound);

        return MapToDto(entity, entity.AttachmentType?.Title ?? string.Empty);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var fullPath = BuildFullPath(entity);
        if (System.IO.File.Exists(fullPath))
            System.IO.File.Delete(fullPath);

        if (!string.IsNullOrEmpty(entity.ThumbnailFileName))
        {
            var thumbPath = Path.Combine(GetRootPath(), entity.EntityId.ToString("N"), "thumbs", entity.ThumbnailFileName);
            if (System.IO.File.Exists(thumbPath))
                System.IO.File.Delete(thumbPath);
        }
    }

    private string GetRootPath()
    {
        return Path.IsPathRooted(_rootPath) ? _rootPath : Path.GetFullPath(_rootPath);
    }

    private string BuildFullPath(EntityAttachment entity)
    {
        return Path.Combine(GetRootPath(), entity.EntityId.ToString("N"), entity.FileName);
    }

    private static string BuildUrl(EntityAttachment entity)
        => $"/uploads/{entity.EntityId:N}/{entity.FileName}";

    private static string? BuildThumbnailUrl(EntityAttachment entity)
        => entity.ThumbnailFileName is null
            ? null
            : $"/uploads/{entity.EntityId:N}/thumbs/{entity.ThumbnailFileName}";

    private static EntityAttachmentDto MapToDto(EntityAttachment entity, string attachmentTypeTitle) => new()
    {
        Id = entity.Id,
        UserId = entity.UserId,
        EntityId = entity.EntityId,
        AttachmentTypeId = entity.AttachmentTypeId,
        AttachmentTypeTitle = attachmentTypeTitle,
        FileName = entity.FileName,
        Extension = entity.Extension,
        Size = entity.Size,
        Url = BuildUrl(entity),
        ThumbnailUrl = BuildThumbnailUrl(entity),
        CreatedAt = entity.CreatedAt
    };
}