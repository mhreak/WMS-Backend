using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.Labels;

public class Label : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Color { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public EntityType EntityType { get; set; }

    public virtual ICollection<EntityLabel> EntityLabels { get; set; } = new List<EntityLabel>();
}
