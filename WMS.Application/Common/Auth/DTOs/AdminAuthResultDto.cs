namespace WMS.Application.Administrator.Auth.DTOs;

public class AdminAuthResultDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;   // ← این خط رو اضافه کن
    public DateTime ExpiresAt { get; set; }
}