using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Labels;

public class EntityLabel
{
    [Required]
    public Guid LabelId { get; set; }

    [ForeignKey(nameof(LabelId))]
    public virtual Label Label { get; set; } = null!;

    [Required]
    public Guid EntityId { get; set; }

    
}
