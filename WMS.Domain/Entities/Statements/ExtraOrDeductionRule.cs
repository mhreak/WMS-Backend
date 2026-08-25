using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Contractors;
using WMS.Domain.Entities.ContractTypes;
using WMS.Domain.Enums;
using WMS.Domain.Entities.Labels;

namespace WMS.Domain.Entities.Statements;

public class ExtraOrDeductionRule : BaseEntity
{
    [Required]
    public Guid ContractTypeId { get; set; }
    [ForeignKey(nameof(ContractTypeId))]
    public virtual ContractType ContractType { get; set; } = null!;

    // اگه پر باشه، قانون فقط مخصوص همین قرارداده؛ خالی یعنی روی کل ContractType اعمال می‌شه
    public Guid? ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public virtual Contract? Contract { get; set; }

    // اگه پر باشه، فقط برای پیمانکار حقیقی/حقوقی اعمال می‌شه
    public ContractorType? ContractorType { get; set; }

    [Required]
    public Guid ExtraOrDeductionTypeId { get; set; }
    [ForeignKey(nameof(ExtraOrDeductionTypeId))]
    public virtual ExtraOrDeductionType ExtraOrDeductionType { get; set; } = null!;

    [Required]
    public AmountType AmountType { get; set; }

    [Required]
    public long Amount { get; set; }

    // دسته‌بندی پیمانکار (ContractorCategory) — اختیاری، برای محدودکردن قانون به یک دسته خاص
    public Guid? CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual ContractorCategory? Category { get; set; }

    // اگه پر باشه، فقط برای همین پیمانکار خاص اعمال می‌شه
    public Guid? ContractorId { get; set; }
    [ForeignKey(nameof(ContractorId))]
    public virtual Contractor? Contractor { get; set; }

    public Guid? LabelId { get; set; }
    [ForeignKey(nameof(LabelId))]
    public virtual Label? Label { get; set; }

    public virtual ICollection<ContractorStatementExtraOrDeduction> StatementItems { get; set; } = new List<ContractorStatementExtraOrDeduction>();
}