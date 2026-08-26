using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.Alarms;

public class DeadlineAlarm : BaseEntity
{
    [Required]
    public EntityType EntityType { get; set; }

    // برای Contract: همون Contract.Id
    // برای ContractStep: همون Contract.Id (والد)، و StepId زیر مشخص می‌کنه کدوم مرحله
    [Required]
    public Guid EntityId { get; set; }

    // فقط وقتی EntityType = ContractStep پر می‌شه (معادل ContractTypeStep.Id)
    public Guid? StepId { get; set; }

    [Required]
    public AlarmReferenceDateType ReferenceDateType { get; set; }

    // مثبت = X روز بعد از تاریخ مرجع | منفی = X روز قبل از تاریخ مرجع
    [Required]
    public int DaysOffset { get; set; }

    public bool IsActive { get; set; } = true;
}