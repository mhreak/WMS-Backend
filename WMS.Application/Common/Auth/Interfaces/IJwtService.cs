using Microsoft.AspNetCore.Identity;
using WMS.Domain.Entities.Users;

namespace WMS.Application.Common.Auth.Interfaces;

public interface IJwtService
{
    Task<string> GenerateAccessToken(User user, UserManager<User> userManager, Guid sessionId, string? activeRole = null);
    DateTime GetAccessTokenExpiry();
    DateTime GetRefreshTokenExpiry();
}
