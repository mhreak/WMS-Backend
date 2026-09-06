using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.File.DTOs;
using WMS.Application.Common.File.Interfaces;
using WMS.Application.Common.File.Validators;
using WMS.Domain.Entities;
using WMS.Domain.Enums;

namespace WMS.Application.Common.File.Services;

public class FileService : IFileService
{
    private readonly IFileAssetRepository _fileRepository;
    private readonly IValidator<GenericUploadFileRequest> _validator;
    private readonly IImageThumbnailService _thumbnailService;
    private readonly string _storageRootPath;

    public FileService(IFileAssetRepository fileRepository, IValidator<GenericUploadFileRequest> validator, IImageThumbnailService thumbnailService, IHostEnvironment hostEnvironment, IConfiguration configuration)
    {
        _fileRepository = fileRepository;
        _validator = validator;
        _thumbnailService = thumbnailService;
        var configuredRoot = configuration["Storage:RootPath"];
        _storageRootPath = string.IsNullOrWhiteSpace(configuredRoot)
            ? Path.GetFullPath(Path.Combine(hostEnvironment.ContentRootPath, "..", "..", "uploads"))
            : Path.GetFullPath(configuredRoot);
    }

    public async Task<FileAsset> UploadForUserAsync(IFormFile file, Guid userId, string userRole, UploadFileType fileType)
    {
        var request = new GenericUploadFileRequest { File = file, FileType = fileType };
        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            var error = result.Errors.First();
            throw new BadRequestException(error.ErrorMessage, error.ErrorCode);
        }
        ValidateUserFileType(userRole, fileType);
        if (ShouldDeleteOldFiles(fileType))
        {
            var existingActiveFiles = await _fileRepository.GetByUploaderIdAndTypeAsync(userId, fileType);
            foreach (var existing in existingActiveFiles)
            {
                existing.IsDeleted = true;
                _fileRepository.Update(existing);
            }
        }
        var relativeFolder = Path.Combine(userRole, userId.ToString(), fileType.ToString());
        var targetFolder = Path.Combine(_storageRootPath, relativeFolder);
        Directory.CreateDirectory(targetFolder);

        var originalExtension = Path.GetExtension(file.FileName);
        var uniqueBaseName = Guid.NewGuid().ToString();

        string filePath;
        string finalExtension;
        string? thumbnailRelativePath = null;

        if (_thumbnailService.IsConvertibleToWebp(originalExtension))
        {
            // عکس رو به webp تبدیل و ذخیره می‌کنیم
            await using (var stream = file.OpenReadStream())
            {
                filePath = await _thumbnailService.SaveAsWebpAsync(
                    stream, Path.Combine(targetFolder, uniqueBaseName), CancellationToken.None);
            }
            finalExtension = ".webp";

            var thumbnailFileName = $"{uniqueBaseName}_thumb.webp";
            var thumbnailFullPath = Path.Combine(targetFolder, "thumbs", thumbnailFileName);
            await _thumbnailService.GenerateThumbnailAsync(filePath, thumbnailFullPath, targetWidth: 800);
            thumbnailRelativePath = Path.Combine("uploads", relativeFolder, "thumbs", thumbnailFileName).Replace("\\", "/");
        }
        else if (_thumbnailService.IsImage(originalExtension)) // فقط gif، بدون تبدیل ولی با Thumbnail
        {
            finalExtension = originalExtension;
            var uniqueFileName = $"{uniqueBaseName}{finalExtension}";
            filePath = Path.Combine(targetFolder, uniqueFileName);
            await using (var fs = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(fs);

            var thumbnailFileName = $"{uniqueBaseName}_thumb.webp";
            var thumbnailFullPath = Path.Combine(targetFolder, "thumbs", thumbnailFileName);
            await _thumbnailService.GenerateThumbnailAsync(filePath, thumbnailFullPath, targetWidth: 800);
            thumbnailRelativePath = Path.Combine("uploads", relativeFolder, "thumbs", thumbnailFileName).Replace("\\", "/");
        }
        else
        {
            // فایل غیرعکسی (PDF, Word, ...) — بدون تغییر
            finalExtension = originalExtension;
            var uniqueFileName = $"{uniqueBaseName}{finalExtension}";
            filePath = Path.Combine(targetFolder, uniqueFileName);
            await using (var fs = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(fs);
        }

        var uniqueFinalFileName = Path.GetFileName(filePath);

        var entity = new FileAsset
        {
            Id = Guid.NewGuid(),
            FileName = uniqueFinalFileName,
            Extension = finalExtension,
            Size = new FileInfo(filePath).Length,   // سایز واقعیِ بعد از تبدیل، نه file.Length اصلی
            UploadFileType = finalExtension == ".webp" ? "image/webp" : file.ContentType,
            Path = Path.Combine("uploads", relativeFolder, uniqueFinalFileName).Replace("\\", "/"),
            ThumbnailPath = thumbnailRelativePath,
            UploaderId = userId,
            OwnerId = userId,
            FileType = fileType,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _fileRepository.AddAsync(entity);
        await _fileRepository.SaveChangesAsync();
        return entity;
    }

    private static bool ShouldDeleteOldFiles(UploadFileType fileType)
        => fileType == UploadFileType.Avatar || fileType == UploadFileType.Cover;

    private static void ValidateUserFileType(string userRole, UploadFileType fileType)
    {
        if (userRole == "Admin")
        {
            return;
        }
        throw new BadRequestException("InvalidRole", "INVALID_ROLE");
    }

    public Task<FileAsset?> GetByIdAsync(Guid id) => _fileRepository.GetByIdAsync(id);
    public Task<List<FileAsset>> GetByUploaderIdAsync(Guid uploaderId) => _fileRepository.GetByUploaderIdAsync(uploaderId);

    public async Task<bool> DeleteAsync(Guid id, Guid currentUserId, bool isAdmin = false)
    {
        var fileAsset = await _fileRepository.GetByIdAsync(id);
        if (fileAsset == null) return false;
        if (!isAdmin && fileAsset.UploaderId != currentUserId && fileAsset.OwnerId != currentUserId)
            throw new UnauthorizedAccessException("Failed");
        fileAsset.IsDeleted = true;
        _fileRepository.Update(fileAsset);
        await _fileRepository.SaveChangesAsync();
        return true;
    }
}
