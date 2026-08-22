using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;

namespace WMS.Persistence.Seeders;

public static class LabelSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Labels.AnyAsync())
            return;

        var labels = new List<Label>
        {
            new Label
            {
                Id = Guid.NewGuid(),
                Name = "پیمانکار اولیه",
                Color = "#28a745",
                IsActive = true,
                EntityType = EntityType.Contractor,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Label
            {
                Id = Guid.NewGuid(),
                Name = "پیمانکار.secondary",
                Color = "#ffc107",
                IsActive = true,
                EntityType = EntityType.Contractor,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Label
            {
                Id = Guid.NewGuid(),
                Name = "قرارداد فعال",
                Color = "#28a745",
                IsActive = true,
                EntityType = EntityType.Contract,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Label
            {
                Id = Guid.NewGuid(),
                Name = "قرارداد منقضی شده",
                Color = "#dc3545",
                IsActive = true,
                EntityType = EntityType.Contract,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Label
            {
                Id = Guid.NewGuid(),
                Name = "قرارداد در حال انجام",
                Color = "#17a2b8",
                IsActive = true,
                EntityType = EntityType.Contract,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Label
            {
                Id = Guid.NewGuid(),
                Name = "قرارداد جدید",
                Color = "#6f42c1",
                IsActive = true,
                EntityType = EntityType.Contract,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.Labels.AddRangeAsync(labels);
        await context.SaveChangesAsync();
    }
}
