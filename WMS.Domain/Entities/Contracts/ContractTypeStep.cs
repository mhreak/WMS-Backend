// WMS.Domain/Entities/Contracts/ContractTypeStep.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Domain.Entities.Contracts;

public class ContractTypeStep : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsMandatory { get; set; }

    [Required]
    public short StepOrder { get; set; }

    [Required]
    public Guid ContractTypeId { get; set; }

    [ForeignKey(nameof(ContractTypeId))]
    public virtual ContractType ContractType { get; set; } = null!;

    /// <summary>
    /// وضعیت شروع این مرحله
    /// </summary>
    public Guid? StartStateId { get; set; }

    [ForeignKey(nameof(StartStateId))]
    public virtual ContractTypeState? StartState { get; set; }

    /// <summary>
    /// وضعیت پایان این مرحله
    /// </summary>
    public Guid? EndStateId { get; set; }

    [ForeignKey(nameof(EndStateId))]
    public virtual ContractTypeState? EndState { get; set; }

    public virtual ICollection<ContractStep> ContractSteps { get; set; } = new List<ContractStep>();
}