using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WMS.Application.Common.Interfaces;
using WMS.Domain.Entities.Users;
using WMS.Persistence.Context;
using WMS.Persistence.Seeders;
using WMS.Persistence.UnitOfWork;

namespace WMS.API.DependencyInjection;

public static class DatabaseDependencyInjection
{
    public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        const int retryCount = 10;
        for (var i = 0; i < retryCount; i++)
        {
            try
            {
                await dbContext.Database.MigrateAsync();
                break;
            }
            catch
            {
                if (i == retryCount - 1) throw;
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        await LocationSeeder.SeedAsync(dbContext);
        await RoleSeeder.SeedRolesAsync(roleManager);
        await DataSeeder.SeedAsync(dbContext);
        // await AdminSeeder.SeedAsync(dbContext, userManager, roleManager);
    }
}
