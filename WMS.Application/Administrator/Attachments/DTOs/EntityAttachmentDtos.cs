using Microsoft.AspNetCore.Http;

namespace WMS.Application.Administrator.Attachments.DTOs;

public class UploadEntityAttachmentRequest
{
    public IFormFile File { get; set; } = null!;
    public Guid EntityId { get; set; }
    public Guid AttachmentTypeId { get; set; }
}

public class EntityAttachmentDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }
    public Guid AttachmentTypeId { get; set; }
    public string AttachmentTypeTitle { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public long Size { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? ThumbnailUrl { get; set; }
}