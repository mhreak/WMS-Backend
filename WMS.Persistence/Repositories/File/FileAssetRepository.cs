using Microsoft.EntityFrameworkCore;
using WMS.Application.Common.File.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Enums;
using WMS.Persistence.Context;
using WMS.Persistence.Repositories;

namespace WMS.Persistence.Repositories.File;

public class FileAssetRepository : BaseRepository<FileAsset>, IFileAssetRepository
{
    public FileAssetRepository(AppDbContext context) : base(context) { }

    public async Task<FileAsset?> GetByIdAsync(Guid id)
    {
        var results = await FindAsync(file => file.Id == id && !file.IsDeleted);
        return results.FirstOrDefault();
    }

    public async Task<List<FileAsset>> GetByUploaderIdAsync(Guid uploaderId)
    {
        return await FindAsync(file => file.UploaderId == uploaderId && !file.IsDeleted);
    }

    public async Task<List<FileAsset>> GetByUploaderIdAndTypeAsync(Guid uploaderId, UploadFileType fileType)
    {
        return await _context.Set<FileAsset>().Where(f => f.UploaderId == uploaderId && f.FileType == fileType && !f.IsDeleted).ToListAsync();
    }
}
