using WMS.Application.Common.Localization;
using WMS.Infrastructure.Localization;

namespace WMS.API.DependencyInjection;

public static class LocalizationDependencyInjection
{
    public static IServiceCollection AddLocalizationDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddMemoryCache();
        var resourcesPath = Path.Combine(AppContext.BaseDirectory, "Resources", "Messages");
        services.AddSingleton<ILocalizationService>(_ => new LocalizationService(resourcesPath));
        services.AddScoped<ICurrentUserLanguageProvider, CurrentUserLanguageProvider>();
        services.AddScoped<IResponseLocalizer, ResponseLocalizer>();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { "fa", "en" };
            options.SetDefaultCulture("fa")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });
        return services;
    }
}
