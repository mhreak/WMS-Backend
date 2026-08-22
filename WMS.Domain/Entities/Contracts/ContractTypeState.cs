
using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Contracts;

public class ContractTypeState : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public virtual ICollection<ContractTypeStep> StartSteps { get; set; } = new List<ContractTypeStep>();
    public virtual ICollection<ContractTypeStep> EndSteps { get; set; } = new List<ContractTypeStep>();
}