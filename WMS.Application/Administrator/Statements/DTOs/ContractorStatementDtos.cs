namespace WMS.Application.Administrator.Statements.DTOs;

public class CreateContractorStatementRequest
{
    public Guid ContractId { get; set; }
    public Guid ContractTypeStepId { get; set; }
    public DateOnly StatementDate { get; set; }
    public Guid FileId { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }
}

public class UpdateContractorStatementRequest
{
    public Guid ContractTypeStepId { get; set; }
    public DateOnly StatementDate { get; set; }
    public Guid FileId { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }
}

public class ContractorStatementFilterRequest
{
    public Guid? ContractId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class SetStatementExtraOrDeductionsRequest
{
    public List<StatementExtraOrDeductionItemRequest> Items { get; set; } = new();
}

public class StatementExtraOrDeductionItemRequest
{
    public Guid RuleId { get; set; }
    public long Amount { get; set; }
}

public class StatementExtraOrDeductionItemDto
{
    public Guid Id { get; set; }
    public Guid RuleId { get; set; }
    public string? RuleTitle { get; set; }
    public bool IsExtra { get; set; }
    public long Amount { get; set; }
}

public class ContractorStatementDto
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string? ContractTitle { get; set; }
    public Guid ContractTypeStepId { get; set; }
    public string? ContractTypeStepTitle { get; set; }
    public DateOnly StatementDate { get; set; }

    public Guid FileId { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }

    public long Amount { get; set; }
    public string? Description { get; set; }
    public List<StatementExtraOrDeductionItemDto> ExtraOrDeductions { get; set; } = new();
    public long NetAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}