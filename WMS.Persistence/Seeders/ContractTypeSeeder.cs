using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Persistence.Seeders;

public static class ContractTypeSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ContractTypes.AnyAsync())
            return;

        var contractTypes = new List<ContractType>
        {
            new ContractType
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد خرید کالا",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractType
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد خدمات",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractType
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد پیمانکاری",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new ContractType
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد اجاره",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.ContractTypes.AddRangeAsync(contractTypes);
        await context.SaveChangesAsync();
    }
}
