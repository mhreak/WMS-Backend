namespace WMS.Application.App.File.DTOs;

public class GenericUserUploadFileRequest
{
    public Microsoft.AspNetCore.Http.IFormFile File { get; set; } = null!;
    public WMS.Domain.Enums.UploadFileType FileType { get; set; }
}
