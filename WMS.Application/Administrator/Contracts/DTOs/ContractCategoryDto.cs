namespace WMS.Application.Administrator.ContractCategories.DTOs;

public class CreateContractCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Guid? ParentId { get; set; }
}

public class UpdateContractCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid? ParentId { get; set; }
}

public class ContractCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid? ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}