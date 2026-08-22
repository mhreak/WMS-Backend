using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Contractors;

namespace WMS.Persistence.Seeders;

public static class ContractSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Contract.AnyAsync())
            return;

        var contractors = await context.Contractors.ToListAsync();
        var categories = await context.ContractCategory.ToListAsync();
        if (!contractors.Any() || !categories.Any())
            return;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var contracts = new List<Contract>
        {
            new Contract
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد خرید تجهیزات انبار",
                ContractNumber = "CON-2026-001",
                ContractAmount = 2500000000,
                ContractorId = contractors[0].Id,
                StartDate = today.AddDays(-30),
                FinishedDate = today.AddDays(60),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contract
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد خدمات نگهداری سیستم",
                ContractNumber = "CON-2026-002",
                ContractAmount = 850000000,
                ContractorId = contractors[1].Id,
                StartDate = today.AddDays(-15),
                FinishedDate = today.AddDays(90),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contract
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد پیمانکاری ساخت انبوه",
                ContractNumber = "CON-2026-003",
                ContractAmount = 5600000000,
                ContractorId = contractors[2].Id,
                StartDate = today.AddDays(10),
                FinishedDate = today.AddDays(180),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contract
            {
                Id = Guid.NewGuid(),
                Title = "قرارداد اجاره لیفتراک",
                ContractNumber = "CON-2026-004",
                ContractAmount = 320000000,
                ContractorId = contractors[3].Id,
                StartDate = today.AddDays(-45),
                FinishedDate = today.AddDays(30),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.Contract.AddRangeAsync(contracts);
        await context.SaveChangesAsync();

        var categoryIds = await context.ContractCategory.Select(c => c.Id).ToListAsync();

        if (categoryIds.Count >= 1)
        {
            var contract0 = await context.Contract.FindAsync(contracts[0].Id);
            var cat0 = await context.ContractCategory.FindAsync(categoryIds[0]);
            if (contract0 != null && cat0 != null)
                contract0.Categories.Add(cat0);
        }

        if (categoryIds.Count >= 2)
        {
            var contract1 = await context.Contract.FindAsync(contracts[1].Id);
            var cat1 = await context.ContractCategory.FindAsync(categoryIds[1]);
            if (contract1 != null && cat1 != null)
                contract1.Categories.Add(cat1);
        }

        if (categoryIds.Count >= 3)
        {
            var contract2 = await context.Contract.FindAsync(contracts[2].Id);
            var cat2 = await context.ContractCategory.FindAsync(categoryIds[2]);
            if (contract2 != null && cat2 != null)
                contract2.Categories.Add(cat2);
        }

        if (categoryIds.Count >= 4)
        {
            var contract3 = await context.Contract.FindAsync(contracts[3].Id);
            var cat3 = await context.ContractCategory.FindAsync(categoryIds[3]);
            if (contract3 != null && cat3 != null)
                contract3.Categories.Add(cat3);
        }

        await context.SaveChangesAsync();
    }
}
