using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Enums;
using WMS.Domain.Entities;

namespace WMS.Persistence.Seeders;

public static class FileAssetSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Files.AnyAsync())
            return;

        var files = new List<FileAsset>
        {
            new FileAsset
            {
                Id = Guid.NewGuid(),
                FileName = "contract-document-001.pdf",
                Extension = ".pdf",
                Size = 245000,
                Path = "uploads/contracts/contract-document-001.pdf",
                UploadFileType = "Post",
                OwnerId = Guid.NewGuid(),
                UploaderId = Guid.NewGuid(),
                FileType = UploadFileType.Post,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new FileAsset
            {
                Id = Guid.NewGuid(),
                FileName = "contractor-avatar.jpg",
                Extension = ".jpg",
                Size = 120000,
                Path = "uploads/avatars/contractor-avatar.jpg",
                UploadFileType = "Avatar",
                OwnerId = Guid.NewGuid(),
                UploaderId = Guid.NewGuid(),
                FileType = UploadFileType.Avatar,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new FileAsset
            {
                Id = Guid.NewGuid(),
                FileName = "contract-cover.png",
                Extension = ".png",
                Size = 540000,
                Path = "uploads/covers/contract-cover.png",
                UploadFileType = "Cover",
                OwnerId = Guid.NewGuid(),
                UploaderId = Guid.NewGuid(),
                FileType = UploadFileType.Cover,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new FileAsset
            {
                Id = Guid.NewGuid(),
                FileName = "category-icon.svg",
                Extension = ".svg",
                Size = 5000,
                Path = "uploads/categories/category-icon.svg",
                UploadFileType = "Category",
                OwnerId = Guid.NewGuid(),
                UploaderId = Guid.NewGuid(),
                FileType = UploadFileType.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.Files.AddRangeAsync(files);
        await context.SaveChangesAsync();
    }
}
