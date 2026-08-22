using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;

namespace WMS.Persistence.Seeders;

public static class EntityLabelSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.EntityLabels.AnyAsync())
            return;

        var labels = await context.Labels.ToListAsync();
        var contractors = await context.Contractors.ToListAsync();
        var contracts = await context.Contract.ToListAsync();

        var entityLabels = new List<EntityLabel>();

        foreach (var label in labels)
        {
            if (label.EntityType == EntityType.Contractor && contractors.Any())
            {
                var target = contractors.Take(2);
                foreach (var c in target)
                {
                    entityLabels.Add(new EntityLabel
                    {

                        LabelId = label.Id,
                        EntityId = c.Id

                    });
                }
            }
            else if (label.EntityType == EntityType.Contract && contracts.Any())
            {
                var target = contracts.Take(3);
                foreach (var c in target)
                {
                    entityLabels.Add(new EntityLabel
                    {
                       
                        LabelId = label.Id,
                        EntityId = c.Id,
                    });
                }
            }
        }

        if (entityLabels.Any())
        {
            await context.EntityLabels.AddRangeAsync(entityLabels);
            await context.SaveChangesAsync();
        }
    }
}
