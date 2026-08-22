using Microsoft.Extensions.Configuration;
using WMS.Application.Administrator.Attachments.DTOs;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Attachments;

namespace WMS.Application.Administrator.Attachments.Services;

public class EntityAttachmentService : IEntityAttachmentService
{
    private readonly IEntityAttachmentRepository _repo;
    private readonly IAttachmentTypeRepository _typeRepo;
    private readonly IUnitOfWork _uow;
    private readonly string _rootPath;

    public EntityAttachmentService(
        IEntityAttachmentRepository repo,
        IAttachmentTypeRepository typeRepo,
        IUnitOfWork uow,
        IConfiguration configuration)
    {
        _repo = repo;
        _typeRepo = typeRepo;
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

        var extension = Path.GetExtension(request.File.FileName).Trim('.').ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(extension))
            throw new BadRequestException(MessageKeys.FileInvalid);

        var storedFileName = $"{Guid.NewGuid():N}.{extension}";

        var entity = new EntityAttachment
        {
            Id = Guid.NewGuid(),
            UserId = currentUserId,
            EntityId = request.EntityId,
            AttachmentTypeId = request.AttachmentTypeId,
            FileName = storedFileName,
            Extension = extension,
            Size = request.File.Length,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        var fullPath = BuildFullPath(entity);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using (var stream = System.IO.File.Create(fullPath))
        {
            await request.File.CopyToAsync(stream, ct);
        }

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
    }

    private string GetRootPath()
    {
        return Path.IsPathRooted(_rootPath)
            ? _rootPath
            : Path.GetFullPath(_rootPath);
    }

    private string BuildFullPath(EntityAttachment entity)
    {
        return Path.Combine(
            GetRootPath(),
            entity.EntityId.ToString("N"),
            entity.FileName);
    }

    private static string BuildUrl(EntityAttachment entity)
        => $"/uploads/{entity.EntityId:N}/{entity.FileName}";

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
        CreatedAt = entity.CreatedAt
    };
}