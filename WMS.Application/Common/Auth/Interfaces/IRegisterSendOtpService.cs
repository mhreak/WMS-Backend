using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Interfaces;

public interface IRegisterSendOtpService
{
    Task<SendOtpForAuthResponseDto> ExecuteAsync(RegisterSendOtpRequestDto request);
}
