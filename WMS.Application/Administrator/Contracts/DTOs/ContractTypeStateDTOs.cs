// WMS.Application/Administrator/ContractTypeStates/DTOs/ContractTypeStateDtos.cs
namespace WMS.Application.Administrator.ContractTypeStates.DTOs;

public class CreateContractTypeStateRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateContractTypeStateRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class ContractTypeStateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ContractTypeStateFilterRequest
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}