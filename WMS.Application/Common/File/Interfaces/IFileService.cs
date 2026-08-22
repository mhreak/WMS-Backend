using Microsoft.AspNetCore.Http;
using WMS.Domain.Entities;
using WMS.Domain.Enums;

namespace WMS.Application.Common.File.Interfaces;

public interface IFileService
{
    Task<FileAsset> UploadForUserAsync(IFormFile file, Guid userId, string userRole, UploadFileType fileType);
    Task<FileAsset?> GetByIdAsync(Guid id);
    Task<List<FileAsset>> GetByUploaderIdAsync(Guid uploaderId);
    Task<bool> DeleteAsync(Guid id, Guid currentUserId, bool isAdmin = false);
}
