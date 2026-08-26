using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Alarms.DTOs;

public class CreateDeadlineAlarmRequest
{
    public EntityType EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid? StepId { get; set; }
    public AlarmReferenceDateType ReferenceDateType { get; set; }
    public int DaysOffset { get; set; }
}

public class DeadlineAlarmDto
{
    public Guid Id { get; set; }
    public EntityType EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid? StepId { get; set; }
    public AlarmReferenceDateType ReferenceDateType { get; set; }
    public int DaysOffset { get; set; }
    public bool IsActive { get; set; }
}

public class DueAlarmDto
{
    public Guid AlarmId { get; set; }
    public EntityType EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid? StepId { get; set; }
    public string? EntityTitle { get; set; }
    public AlarmReferenceDateType ReferenceDateType { get; set; }
    public DateOnly ReferenceDate { get; set; }
    public DateOnly TargetDate { get; set; }
    public int RemainingDays { get; set; }
}