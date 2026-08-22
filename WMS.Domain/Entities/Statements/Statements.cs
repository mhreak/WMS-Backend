using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;
using WMS.Domain.Entities.Attachments;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Domain.Entities.Statements;

public class ContractorStatement : BaseEntity
{
    [Required]
    public Guid ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public virtual Contract Contract { get; set; } = null!;

    [Required]
    public Guid ContractTypeStepId { get; set; }
    [ForeignKey(nameof(ContractTypeStepId))]
    public virtual ContractTypeStep ContractTypeStep { get; set; } = null!;

    [Required]
    public DateOnly StatementDate { get; set; }

    public Guid? FileId { get; set; }
    [ForeignKey(nameof(FileId))]
    public virtual EntityAttachment Attachment { get; set; } = null!;

    [Required]
    public long Amount { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public virtual ICollection<ContractorStatementExtraOrDeduction> ExtraOrDeductions { get; set; } = new List<ContractorStatementExtraOrDeduction>();
}