// WMS.Application/Administrator/Contracts/DTOs/ContractStepDtos.cs
namespace WMS.Application.Administrator.Contracts.DTOs;

public class CreateContractStepRequest
{
    public Guid ContractId { get; set; }
    public Guid StepId { get; set; }          // ContractTypeStepId
    public DateOnly StartDate { get; set; }
    public DateOnly? FinishDate { get; set; }
    public DateOnly? DeadlineDate { get; set; }
}

public class UpdateContractStepRequest
{
    public DateOnly StartDate { get; set; }
    public DateOnly FinishDate { get; set; }
    public DateOnly? DeadlineDate { get; set; }
}

public class ContractStepDto
{
    public Guid ContractId { get; set; }
    public Guid StepId { get; set; }
    public string? StepTitle { get; set; }
    public short? StepOrder { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? FinishDate { get; set; }
    public DateOnly? DeadlineDate { get; set; }
}

public class SetContractStepsRequest
{
    public Guid ContractId { get; set; }
    public List<ContractStepItemRequest> Steps { get; set; } = new();
}

public class ContractStepItemRequest
{
    public Guid StepId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly FinishDate { get; set; }
}