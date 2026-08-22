namespace WMS.Application.Administrator.ContractBoard.DTOs;

public class ContractBoardFilterRequest
{
    public Guid ContractTypeId { get; set; }
    public Guid? ContractorId { get; set; }
}

public class ContractBoardResponse
{
    public Guid ContractTypeId { get; set; }
    public string? ContractTypeTitle { get; set; }
    public List<ContractBoardStepDto> Steps { get; set; } = new();
}

public class ContractBoardStepDto
{
    public Guid StepId { get; set; }
    public string Title { get; set; } = string.Empty;
    public short StepOrder { get; set; }
    public int ContractsCount { get; set; }
    public List<ContractBoardCardDto> Contracts { get; set; } = new();
}

public class ContractBoardCardDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? ContractNumber { get; set; }
    public string? ContractorName { get; set; }
    public Guid? ContractorId { get; set; }
    public DateOnly? StartDate { get; set; }      // شروع این مرحله
    public DateOnly? FinishedDate { get; set; }   // اگر مرحله تمام شده
    public DateOnly? ContractStartDate { get; set; }
}

public class MoveContractStepRequest
{
   
    public Guid TargetStepId { get; set; }

        public DateOnly? FinishedDate { get; set; }

    public DateOnly? StartDate { get; set; }
}