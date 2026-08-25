using WMS.Domain.Entities.Alarms;

namespace WMS.Application.Administrator.Alarms.DTOs;

public class CreateDeadlineAlarmRequest
{
    public AlarmEntityType EntityType { get; set; }
    public Guid ContractId { get; set; }
    public Guid? StepId { get; set; }
    public int DaysBeforeDeadline { get; set; }
}

public class DeadlineAlarmDto
{
    public Guid Id { get; set; }
    public AlarmEntityType EntityType { get; set; }
    public Guid ContractId { get; set; }
    public Guid? StepId { get; set; }
    public int DaysBeforeDeadline { get; set; }
    public bool IsActive { get; set; }
}

// خروجی API «الارم‌های سررسیدشده»
public class DueAlarmDto
{
    public Guid AlarmId { get; set; }
    public AlarmEntityType EntityType { get; set; }
    public Guid ContractId { get; set; }
    public string? ContractTitle { get; set; }
    public Guid? StepId { get; set; }
    public string? StepTitle { get; set; }
    public DateOnly DeadlineDate { get; set; }
    public int RemainingDays { get; set; }
    public int DaysBeforeDeadline { get; set; }
}