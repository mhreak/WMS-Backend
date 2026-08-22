using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Persistence.Seeders;

public static class ContractStepSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Contract_ContractTypeStep.AnyAsync())
            return;

        var contracts = await context.Contract.ToListAsync();
        var contractTypeSteps = await context.ContractTypeStep.ToListAsync();
        if (!contracts.Any() || !contractTypeSteps.Any())
            return;

        var contractSteps = new List<ContractStep>();
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (var contract in contracts)
        {
            var stepsForContract = contractTypeSteps.Take(3).ToList();
            var startDate = contract.StartDate ?? today;
            var finishDate = contract.FinishedDate ?? today.AddDays(90);

            var daysSpan = (finishDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;
            var stepSpan = daysSpan / stepsForContract.Count;

            for (int i = 0; i < stepsForContract.Count; i++)
            {
                var stepStart = startDate.AddDays(i * stepSpan);
                var stepFinish = startDate.AddDays((i + 1) * stepSpan);
                if (i == stepsForContract.Count - 1)
                    stepFinish = finishDate;

                contractSteps.Add(new ContractStep
                {
                    ContractId = contract.Id,
                    StepId = stepsForContract[i].Id,
                    StartDate = stepStart,
                    FinishDate = stepFinish
                });
            }
        }

        await context.Contract_ContractTypeStep.AddRangeAsync(contractSteps);
        await context.SaveChangesAsync();
    }
}
