using Microsoft.AspNetCore.Http;
using WMS.Domain.Enums;

namespace WMS.Application.Common.File.DTOs;

public class GenericUploadFileRequest
{
    public IFormFile File { get; set; } = null!;
    public UploadFileType FileType { get; set; }
}
