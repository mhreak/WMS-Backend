using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;
using WMS.Domain.Entities.Contracts;

namespace WMS.Domain.Entities.ContractTypes;

public class ContractType : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public virtual ICollection<ContractTypeStep> Steps { get; set; } = new List<ContractTypeStep>();
}
