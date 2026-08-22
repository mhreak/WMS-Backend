using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;
using WMS.Domain.Entities.Contractors;

namespace WMS.Domain.Entities.Contracts;

public class Contract : BaseEntity
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    public Guid? ContractorId { get; set; }

    [ForeignKey(nameof(ContractorId))]
    public virtual Contractor? Contractor { get; set; }

    public long? ContractAmount { get; set; }

    [Required]
    [MaxLength(20)]
    public string ContractNumber { get; set; } = string.Empty;

    public DateOnly? StartDate { get; set; }

    public DateOnly? FinishedDate { get; set; }

    public virtual ICollection<ContractStep> ContractSteps { get; set; } = new List<ContractStep>();

    public virtual ICollection<ContractCategory> Categories { get; set; } = new List<ContractCategory>();

    public Guid? ContractTypeStateId { get; set; }

    [ForeignKey(nameof(ContractTypeStateId))]
    public virtual ContractTypeState? ContractTypeState { get; set; }
}
