// WMS.Application/Administrator/Steps/DTOs/StepDtos.cs
namespace WMS.Application.Administrator.ContractTypeSteps.DTOs;

public class CreateStepRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public byte StepOrder { get; set; }
    public bool IsMandatory { get; set; }
    public Guid ContractTypeId { get; set; }
    public Guid? StartStateId { get; set; }
    public Guid? EndStateId { get; set; }

    public string StartStateTitle { get; set; } = string.Empty;
    public string EndStateTitle { get; set; } = string.Empty;
}

public class UpdateStepRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public byte StepOrder { get; set; }
     public bool IsMandatory { get; set; }
    public Guid ContractTypeId { get; set; }
    public Guid? StartStateId { get; set; }
    public Guid? EndStateId { get; set; }
    public string StartStateTitle { get; set; } = string.Empty;
    public string EndStateTitle { get; set; } = string.Empty;
}

public class StepDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public short StepOrder { get; set; }
    public bool IsMandatory { get; set; }
    public Guid ContractTypeId { get; set; }
    public string? ContractTypeTitle { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? StartStateId { get; set; }
    public Guid? EndStateId { get; set; }
    public string StartStateTitle { get; set; } = string.Empty;
    public string EndStateTitle { get; set; } = string.Empty;
}

public class StepFilterRequest
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
    public Guid? ContractTypeId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}