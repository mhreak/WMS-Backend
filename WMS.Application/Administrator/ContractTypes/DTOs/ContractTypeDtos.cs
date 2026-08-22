// WMS.Application/Administrator/ContractTypes/DTOs/ContractTypeDtos.cs
namespace WMS.Application.Administrator.ContractTypes.DTOs;

public class CreateContractTypeRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateContractTypeRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class ContractTypeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ContractTypeFilterRequest
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}