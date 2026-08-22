using WMS.Domain.Entities;
using WMS.Domain.Enums;

namespace WMS.Application.Common.File.Interfaces;

public interface IFileAssetRepository : WMS.Application.Common.Interfaces.Repositories.IGenericRepository<FileAsset>
{
    Task<FileAsset?> GetByIdAsync(Guid id);
    Task<List<FileAsset>> GetByUploaderIdAsync(Guid uploaderId);
    Task<List<FileAsset>> GetByUploaderIdAndTypeAsync(Guid uploaderId, UploadFileType fileType);
}
