using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Attachments.DTOs;

public class CreateAttachmentTypeRequest
{
    public EntityType EntityType { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateAttachmentTypeRequest
{
    public EntityType EntityType { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class AttachmentTypeDto
{
    public Guid Id { get; set; }
    public EntityType EntityType { get; set; }
    public string EntityTypeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}