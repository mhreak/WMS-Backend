using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Persistence.Seeders;

namespace WMS.Persistence.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await ContractorCategorySeeder.SeedAsync(context);
        await LabelSeeder.SeedAsync(context);
        await ContractorSeeder.SeedAsync(context);
        await ContractTypeSeeder.SeedAsync(context);
        await ContractTypeStepSeeder.SeedAsync(context);
        await ContractCategorySeeder.SeedAsync(context);
        await ContractSeeder.SeedAsync(context);
        await ContractStepSeeder.SeedAsync(context);
        await EntityLabelSeeder.SeedAsync(context);
        await FileAssetSeeder.SeedAsync(context);
        // await EntityAttachmentSeeder.SeedAsync(context);
    }
}
