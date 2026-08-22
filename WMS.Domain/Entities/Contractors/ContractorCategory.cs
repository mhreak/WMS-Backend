using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Contractors;

public class ContractorCategory : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public Guid? ParentId { get; set; }
    public virtual ContractorCategory? Parent { get; set; }
    public virtual ICollection<ContractorCategory> Children { get; set; } = new List<ContractorCategory>();

    public virtual ICollection<Contractor> Contractors { get; set; } = new List<Contractor>();
}