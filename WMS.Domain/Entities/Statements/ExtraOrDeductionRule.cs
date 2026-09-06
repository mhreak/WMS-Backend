using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Contractors;
using WMS.Domain.Entities.ContractTypes;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.Statements;

public class ExtraOrDeductionRule : BaseEntity
{
    [Required]
    public Guid ContractTypeId { get; set; }
    [ForeignKey(nameof(ContractTypeId))]
    public virtual ContractType ContractType { get; set; } = null!;

    // حذف شد: ContractId

    public ContractorType? ContractorType { get; set; }

    [Required]
    public Guid ExtraOrDeductionTypeId { get; set; }
    [ForeignKey(nameof(ExtraOrDeductionTypeId))]
    public virtual ExtraOrDeductionType ExtraOrDeductionType { get; set; } = null!;

    [Required]
    public AmountType AmountType { get; set; }

    [Required]
    public long Amount { get; set; }

    // دسته‌بندی پیمانکار
    public Guid? ContractorCategoryId { get; set; }
    [ForeignKey(nameof(ContractorCategoryId))]
    public virtual ContractorCategory? ContractorCategory { get; set; }

    // دسته‌بندی قرارداد (جدید)
    public Guid? ContractCategoryId { get; set; }
    [ForeignKey(nameof(ContractCategoryId))]
    public virtual ContractCategory? ContractCategory { get; set; }

    public Guid? ContractorId { get; set; }
    [ForeignKey(nameof(ContractorId))]
    public virtual Contractor? Contractor { get; set; }

    // لیبل قرارداد
    public Guid? ContractLabelId { get; set; }
    [ForeignKey(nameof(ContractLabelId))]
    public virtual Label? ContractLabel { get; set; }

    public virtual ICollection<ContractorStatementExtraOrDeduction> StatementItems { get; set; }
        = new List<ContractorStatementExtraOrDeduction>();
}