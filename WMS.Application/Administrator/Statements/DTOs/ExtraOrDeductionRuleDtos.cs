using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Statements.DTOs;

public class CreateExtraOrDeductionRuleRequest
{
    public Guid ContractTypeId { get; set; }
    public Guid? ContractId { get; set; }
    public ContractorType? ContractorType { get; set; }
    public Guid ExtraOrDeductionTypeId { get; set; }
    public AmountType AmountType { get; set; }
    public long Amount { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? ContractorId { get; set; }
    public Guid? LabelId { get; set; }
    public string? LabelName { get; set; }
}

public class UpdateExtraOrDeductionRuleRequest : CreateExtraOrDeductionRuleRequest
{
}

public class ExtraOrDeductionRuleFilterRequest
{
    public Guid? ContractTypeId { get; set; }
    public Guid? ContractId { get; set; }
    public Guid? ExtraOrDeductionTypeId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class ExtraOrDeductionRuleDto
{
    public Guid Id { get; set; }
    public Guid ContractTypeId { get; set; }
    public string? ContractTypeTitle { get; set; }
    public Guid? ContractId { get; set; }
    public string? ContractTitle { get; set; }
    public ContractorType? ContractorType { get; set; }
    public Guid ExtraOrDeductionTypeId { get; set; }
    public string? ExtraOrDeductionTypeTitle { get; set; }
    public bool IsExtra { get; set; }
    public AmountType AmountType { get; set; }
    public long Amount { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? LabelId { get; set; }
    public string? LabelName { get; set; }
}