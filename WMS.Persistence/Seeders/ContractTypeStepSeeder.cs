using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Seeders;

public static class ContractTypeStepSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ContractTypeStep.AnyAsync())
            return;

        var contractTypes = await context.ContractTypes.ToListAsync();
        if (!contractTypes.Any())
            return;

        var steps = new List<ContractTypeStep>();

        foreach (var contractType in contractTypes)
        {
            var stepCount = 3 + Random.Shared.Next(0, 2);
            for (short i = 1; i <= stepCount; i++)
            {
                steps.Add(new ContractTypeStep
                {
                    Id = Guid.NewGuid(),
                    Title = $"مرحله {i} - {contractType.Title}",
                    IsActive = true,
                    ContractTypeId = contractType.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                });
            }
        }

        await context.ContractTypeStep.AddRangeAsync(steps);
        await context.SaveChangesAsync();
    }
}
