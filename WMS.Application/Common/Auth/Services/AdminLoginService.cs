using Microsoft.AspNetCore.Identity;
using WMS.Application.Administrator.Auth.DTOs;
using WMS.Application.Administrator.Auth.Interfaces;
using WMS.Application.Common.Auth;
using WMS.Application.Common.Auth.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Users;

namespace WMS.Application.Administrator.Auth.Services;

public class AdminLoginService : IAdminLoginService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AdminLoginService(UserManager<User> userManager, IJwtService jwtService, IRefreshTokenService refreshTokenService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<AdminAuthResultDto> LoginAsync(AdminLoginRequestDto request)
{
    var admin = await _userManager.FindByNameAsync(request.Username);
    if (admin == null)
        throw new UnauthorizedAccessException(MessageKeys.InvalidCredentials);

    if (!admin.IsActive)
        throw new UnauthorizedAccessException(MessageKeys.AccountInactive);

    var validPassword = await _userManager.CheckPasswordAsync(admin, request.Password);
    if (!validPassword)
        throw new UnauthorizedAccessException(MessageKeys.InvalidCredentials);

    var isAdmin = await _userManager.IsInRoleAsync(admin, "Admin");
    if (!isAdmin)
        throw new UnauthorizedAccessException(MessageKeys.NoAccess);

    // این خط قبلاً هم بود
    var session = await _refreshTokenService.IssueAsync(admin.Id, "Admin");

    var token = await _jwtService.GenerateAccessToken(admin, _userManager, session.SessionId);

    return new AdminAuthResultDto
    {
        AccessToken = token,
        RefreshToken = session.RefreshToken,          // ← این خط مهمه
        ExpiresAt = _jwtService.GetAccessTokenExpiry()
    };
}
}
