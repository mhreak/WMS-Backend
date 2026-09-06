using WMS.Domain.Enums;

public class CreateExtraOrDeductionRuleRequest
{
    public Guid ContractTypeId { get; set; }
    // ContractId حذف شد
    public ContractorType? ContractorType { get; set; }
    public Guid ExtraOrDeductionTypeId { get; set; }
    public AmountType AmountType { get; set; }
    public long Amount { get; set; }

    public Guid? ContractorCategoryId { get; set; }   // قبلاً CategoryId
    public Guid? ContractCategoryId { get; set; }     // جدید
    public Guid? ContractorId { get; set; }
    public Guid? ContractLabelId { get; set; }        // قبلاً LabelId
}

public class UpdateExtraOrDeductionRuleRequest : CreateExtraOrDeductionRuleRequest
{
}

public class ExtraOrDeductionRuleFilterRequest
{
    public Guid? ContractTypeId { get; set; }
    // ContractId حذف شد
    public Guid? ExtraOrDeductionTypeId { get; set; }
    public Guid? ContractorCategoryId { get; set; }
    public Guid? ContractCategoryId { get; set; }
    public Guid? ContractLabelId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class ExtraOrDeductionRuleDto
{
    public Guid Id { get; set; }
    public Guid ContractTypeId { get; set; }
    public string? ContractTypeTitle { get; set; }

    // ContractId / ContractTitle حذف شد

    public ContractorType? ContractorType { get; set; }
    public Guid ExtraOrDeductionTypeId { get; set; }
    public string? ExtraOrDeductionTypeTitle { get; set; }
    public bool IsExtra { get; set; }
    public AmountType AmountType { get; set; }
    public long Amount { get; set; }

    public Guid? ContractorCategoryId { get; set; }
    public string? ContractorCategoryName { get; set; }

    public Guid? ContractCategoryId { get; set; }
    public string? ContractCategoryName { get; set; }

    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; set; }

    public Guid? ContractLabelId { get; set; }
    public string? ContractLabelName { get; set; }

    public DateTime CreatedAt { get; set; }
}