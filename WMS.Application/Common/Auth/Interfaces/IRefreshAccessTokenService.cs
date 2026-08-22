using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Interfaces;

public interface IRefreshAccessTokenService
{
    Task<AuthResultDto> RefreshAsync(RefreshTokenRequestDto request);
}
