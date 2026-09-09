using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Statements.DTOs;

public class CreateContractorStatementRequest
{
    public Guid ContractId { get; set; }
    public Guid ContractTypeStepId { get; set; }
    public DateOnly StatementDate { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid? FileId { get; set; }
}

public class UpdateContractorStatementRequest
{
    public Guid ContractTypeStepId { get; set; }
    public DateOnly StatementDate { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid? FileId { get; set; }
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
    public AmountType AmountType { get; set; }
    public long? PercentageValue { get; set; } // فقط وقتی AmountType = Percentage
    public long Amount { get; set; }           // مبلغ نهایی محاسبه‌شده (ریال)
}

public class StatementAttachmentDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public long Size { get; set; }
}
public class ContractorStatementDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid ContractId { get; set; }
    public string? ContractTitle { get; set; }
    public Guid ContractTypeStepId { get; set; }
    public string? ContractTypeStepTitle { get; set; }
    public DateOnly StatementDate { get; set; }
    // public string? FilePath { get; set; }
    // public List<StatementAttachmentDto> Attachments { get; set; } = new();
    public Guid? FileId { get; set; }
    public FileInfoDto? File { get; set; }
    public long Amount { get; set; }      // مبلغ ناخالص (Amount اصلی)
    public List<StatementExtraOrDeductionItemDto> Extras { get; set; } = new();       // اضافات
    public List<StatementExtraOrDeductionItemDto> Deductions { get; set; } = new();   // کسورات
    public long TotalExtra { get; set; }
    public long TotalDeduction { get; set; }
    public long NetAmount { get; set; }        // مبلغ نهایی قابل پرداخت

    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}