using WMS.Application.App.Auth.DTOs;
using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.App.CustomerModule.Auth.Interfaces;

public interface ICustomerAuthService
{
    Task<SendOtpForAuthResponseDto> SendOtpForLoginAsync(SendOtpForAuthRequestDto request);
    Task<AuthResultDto> LoginOtpAsync(LoginOtpRequestDto request);
    Task<AuthResultDto> LoginByPasswordAsync(LoginByPasswordRequestDto request);
    Task<AuthResultDto> RegisterByMobileAsync(RegisterByMobileRequestDto request);
    Task<AuthResultDto> ChangePasswordAsync(Guid userId, ChangePasswordRequestDto request, CancellationToken cancellationToken);
    Task<GoogleAuthResultDto> GoogleAuthAsync(GoogleAuthRequestDto request);
}
