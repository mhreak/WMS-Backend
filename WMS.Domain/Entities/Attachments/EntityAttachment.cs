// WMS.Domain/Entities/Attachments/EntityAttachment.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Attachments;

public class EntityAttachment : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }

    public Guid AttachmentTypeId { get; set; }
    public virtual AttachmentType AttachmentType { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Extension { get; set; } = string.Empty;

    public long Size { get; set; }

    public string? ThumbnailFileName { get; set; }
}
