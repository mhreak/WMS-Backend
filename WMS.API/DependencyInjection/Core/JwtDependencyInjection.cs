using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Security.Claims;
using System.Text;
using WMS.Application.Common.Auth.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Users;
using WMS.Infrastructure.Authentication;

namespace WMS.API.DependencyInjection;

public static class JwtDependencyInjection
{
    public static IServiceCollection AddJwtDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<WMS.Infrastructure.Authentication.GoogleAuthSettings>(configuration.GetSection("GoogleAuth"));
        services.AddScoped<IJwtService, JwtService>();
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
            ?? throw new InvalidOperationException(MessageKeys.JwtSettingsNotConfigured);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(token))
                            context.Token = token.Replace("Bearer ", "");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var sid = context.Principal?.FindFirstValue("sid");
                        if (sid == null) { context.Fail("Invalid token."); return; }
                        var redis = context.HttpContext.RequestServices.GetRequiredService<IConnectionMultiplexer>();
                        var db = redis.GetDatabase();
                        var sessionExists = await db.KeyExistsAsync($"session:{sid}");
                        if (!sessionExists)
                            context.Fail("Session revoked.");
                    }
                };
            });
        services.AddAuthorization();
        return services;
    }
}
