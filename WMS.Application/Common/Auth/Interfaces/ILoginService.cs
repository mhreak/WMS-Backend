using WMS.Application.App.Auth.DTOs;
using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Interfaces;

public interface ILoginService
{
    Task<AuthResultDto> LoginAsync(LoginRequestDto request);
}
