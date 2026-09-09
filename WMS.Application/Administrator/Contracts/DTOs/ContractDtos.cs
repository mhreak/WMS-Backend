
// WMS.Application/Administrator/Contracts/DTOs/ContractDtos.cs
using System.Text.Json.Serialization;
using WMS.Application.Administrator.Contractors.DTOs;

namespace WMS.Application.Administrator.Contracts.DTOs;

public class CreateContractRequest
{
    public string Title { get; set; } = string.Empty;
    public Guid? ContractorId { get; set; }
    public long ContractAmount { get; set; }
    public string ContractNumber { get; set; } = string.Empty;

    // تغییر کرد
    public DateTime? StartDate { get; set; }
    public DateTime? FinishedDate { get; set; }

    public Guid? ContractTypeId { get; set; }
    public List<Guid>? CategoryIds { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? ContractTypeStateId { get; set; }
    public Guid? FileId { get; set; }
    public Guid? AttachmentFileId { get; set; }
}

public class UpdateContractRequest
{
    public string Title { get; set; } = string.Empty;
    public Guid? ContractorId { get; set; }
    public long ContractAmount { get; set; }
    public string ContractNumber { get; set; } = string.Empty;

    // تغییر کرد
    public DateTime? StartDate { get; set; }
    public DateTime? FinishedDate { get; set; }

    public Guid? ContractTypeId { get; set; }
    public bool IsActive { get; set; }
    public List<Guid>? CategoryIds { get; set; }
    public Guid? ContractTypeStateId { get; set; }
    public Guid? FileId { get; set; }
    public Guid? AttachmentFileId { get; set; }
}

public class FileInfoDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string? ThumbnailPath { get; set; }
    public string Extension { get; set; } = string.Empty;
    public long Size { get; set; }
}

public class ContractDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; set; }

    public long ContractAmount { get; set; }
    public string ContractNumber { get; set; } = string.Empty;

    public List<LookupItemDto> Categories { get; set; } = new();

    // تغییر کرد
    public DateOnly? StartDate { get; set; }
    public DateOnly? FinishedDate { get; set; }

    public bool IsActive { get; set; }

    public Guid? ContractTypeId { get; set; }
    public string ContractTypeTitle { get; set; } = string.Empty;

    public Guid? CurrentStepId { get; set; }
    public string? CurrentStepTitle { get; set; }
    public DateOnly? CurrentStepStartDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? ContractTypeStateId { get; set; }
    public string ContractTypeStateTitle { get; set; } = string.Empty;

    public Guid? FileId { get; set; }
    public FileInfoDto? File { get; set; }

    public Guid? AttachmentFileId { get; set; }
    public FileInfoDto? AttachmentFile { get; set; }
}
public class ContractFilterRequest
{
    public string? Search { get; set; }
    public Guid? ContractorId { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
