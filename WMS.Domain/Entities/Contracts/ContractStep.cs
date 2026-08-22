// WMS.Domain/Entities/Contracts/ContractStep.cs
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Entities.Contracts;

namespace WMS.Domain.Entities.Contracts;

public class ContractStep
{
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; } = null!;

    public Guid StepId { get; set; }
    public ContractTypeStep Step { get; set; } = null!;

    public DateOnly StartDate { get; set; }
    public DateOnly? FinishDate { get; set; }

    public DateOnly? DeadlineDate { get; set; }
}