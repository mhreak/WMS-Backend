using Microsoft.AspNetCore.Identity;
using WMS.Domain.Entities.Users;
using WMS.Persistence.Context;

namespace WMS.API.DependencyInjection;

public static class IdentityDependencyInjection
{
    public static IServiceCollection AddIdentityDependencies(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
        return services;
    }
}
