using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Contracts;

public class ContractCategory : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public Guid? ParentId { get; set; }
    public virtual ContractCategory? Parent { get; set; }
    public virtual ICollection<ContractCategory> Children { get; set; } = new List<ContractCategory>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}