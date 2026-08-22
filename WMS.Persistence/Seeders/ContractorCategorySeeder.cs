using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Contractors;

namespace WMS.Persistence.Seeders;

public static class ContractorCategorySeeder
{
    private static readonly Guid ContractorCategoryGuid = new("b8d34e68-1a2b-4c5d-9e6f-8a7b6c5d4e3f");

    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ContractorCategories.AnyAsync())
            return;

        var categories = new List<ContractorCategory>
        {
            new ContractorCategory
            {
                Id = ContractorCategoryGuid,
                Name = "پیمانکار داخلی",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractorCategory
            {
                Id = new Guid("c9e45f79-2b3c-5d6e-0f7a-9b8c7d6e5f4a"),
                Name = "پیمانکار خارجی",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractorCategory
            {
                Id = new Guid("d0f56a8a-3c4d-6e7f-1a8b-0c9d8e7f6a5b"),
                Name = "سازنده",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractorCategory
            {
                Id = new Guid("e1a67b9b-4d5e-7f8a-2b9c-1d0e9f8a7b6c"),
                Name = "تأمین کننده",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.ContractorCategories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
