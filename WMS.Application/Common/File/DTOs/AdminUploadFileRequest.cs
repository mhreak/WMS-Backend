namespace WMS.Application.Administrator.File.DTOs;

public class AdminUploadFileRequest
{
    public Microsoft.AspNetCore.Http.IFormFile File { get; set; } = null!;
    public WMS.Domain.Enums.UploadFileType FileType { get; set; }
}
