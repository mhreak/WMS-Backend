namespace WMS.Application.Administrator.Auth.Interfaces;

public interface IAdminRegisterService
{
    Task RegisterAsync(WMS.Application.Administrator.Auth.DTOs.AdminRegisterRequestDto request);
}
