// WMS.Domain/Entities/Attachments/AttachmentType.cs
using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.Attachments;

public class AttachmentType : BaseEntity
{
    [Required]
    public EntityType EntityType { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public virtual ICollection<EntityAttachment> EntityAttachments { get; set; } = new List<EntityAttachment>();
}