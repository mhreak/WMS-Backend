// WMS.Application/Administrator/Contractors/DTOs/ContractorCategoryDtos.cs
namespace WMS.Application.Administrator.Contractors.DTOs;

public class CreateContractorCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Guid? ParentId { get; set; }
}

public class UpdateContractorCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid? ParentId { get; set; }
}

public class ContractorCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid? ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}