using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Statements;

public class ExtraOrDeductionType : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // true = اضافه (Extra) | false = کسر (Deduction)
    public bool IsExtra { get; set; }

    public virtual ICollection<ExtraOrDeductionRule> Rules { get; set; } = new List<ExtraOrDeductionRule>();
}