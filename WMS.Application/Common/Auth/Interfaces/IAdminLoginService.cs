using WMS.Application.Administrator.Auth.DTOs;

namespace WMS.Application.Administrator.Auth.Interfaces;

public interface IAdminLoginService
{
    Task<AdminAuthResultDto> LoginAsync(AdminLoginRequestDto request);
}
