using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Statements;

public class ContractorStatementExtraOrDeduction : BaseEntity
{
    [Required]
    public Guid RuleId { get; set; }
    [ForeignKey(nameof(RuleId))]
    public virtual ExtraOrDeductionRule Rule { get; set; } = null!;

    [Required]
    public Guid StatementId { get; set; }
    [ForeignKey(nameof(StatementId))]
    public virtual ContractorStatement Statement { get; set; } = null!;

    [Required]
    public long Amount { get; set; }
}