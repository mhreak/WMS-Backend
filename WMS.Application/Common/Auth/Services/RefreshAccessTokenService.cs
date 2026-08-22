using Microsoft.AspNetCore.Identity;
using WMS.Application.Common.Auth.DTOs;
using WMS.Application.Common.Auth.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Models;
using WMS.Domain.Entities.Users;

namespace WMS.Application.Common.Auth.Services;

public class RefreshAccessTokenService : IRefreshAccessTokenService
{
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    public RefreshAccessTokenService(IRefreshTokenService refreshTokenService, UserManager<User> userManager, IJwtService jwtService)
    {
        _refreshTokenService = refreshTokenService;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthResultDto> RefreshAsync(RefreshTokenRequestDto request)
    {
        var rotation = await _refreshTokenService.RotateAsync(request.RefreshToken);
        if (rotation.Status == RefreshTokenRotationStatus.Invalid)
            throw new BadRequestException(MessageKeys.InvalidCredentials, "invalid_refresh_token");
        var user = await _userManager.FindByIdAsync(rotation.UserId.ToString());
        if (user is null)
            throw new BadRequestException(MessageKeys.UserNotFound, "user_not_found");
        var accessToken = await _jwtService.GenerateAccessToken(user, _userManager, rotation.SessionId, rotation.Role);
        return new AuthResultDto { AccessToken = accessToken, RefreshToken = rotation.NewRefreshToken!, ExpiresAt = _jwtService.GetAccessTokenExpiry() };
    }
}
