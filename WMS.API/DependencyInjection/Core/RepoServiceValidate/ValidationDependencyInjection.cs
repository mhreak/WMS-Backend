using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WMS.API.Filters;
using WMS.Application.Common.Auth.Validators;

namespace WMS.API.DependencyInjection;

public static class ValidationDependencyInjection
{
    public static IServiceCollection AddValidationDependencies(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginOtpRequestValidator>();
        services.AddScoped<ValidationFilter>();
        services.Configure<MvcOptions>(options => { options.Filters.Add<ValidationFilter>(); });
        return services;
    }
}
