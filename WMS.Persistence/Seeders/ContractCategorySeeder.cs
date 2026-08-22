using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Seeders;

public static class ContractCategorySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ContractCategory.AnyAsync())
            return;

        var categories = new List<ContractCategory>
        {
            new ContractCategory
            {
                Id = Guid.NewGuid(),
                Name = "قراردادهای خرید",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractCategory
            {
                Id = Guid.NewGuid(),
                Name = "قراردادهای خدمات فنی",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractCategory
            {
                Id = Guid.NewGuid(),
                Name = "قراردادهای پیمانکاری",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractCategory
            {
                Id = Guid.NewGuid(),
                Name = "قراردادهای اجاره تجهیزات",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.ContractCategory.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
