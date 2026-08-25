using WMS.Application.Administrator.Alarms.DTOs;
using WMS.Application.Administrator.Alarms.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Alarms;

namespace WMS.Application.Administrator.Alarms.Services;

public class DeadlineAlarmService : IDeadlineAlarmService
{
    private readonly IDeadlineAlarmRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeadlineAlarmService(IDeadlineAlarmRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<DeadlineAlarmDto> CreateAsync(CreateDeadlineAlarmRequest request, CancellationToken ct = default)
    {
        var entity = new DeadlineAlarm
        {
            Id = Guid.NewGuid(),
            EntityType = request.EntityType,
            ContractId = request.ContractId,
            StepId = request.StepId,
            DaysBeforeDeadline = request.DaysBeforeDeadline,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task<DeadlineAlarmDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.DeadlineAlarmNotFound);

        return MapToDto(entity);
    }

    public async Task<List<DueAlarmDto>> GetDueAlarmsAsync(CancellationToken ct = default)
    {
        var alarms = await _repo.GetActiveAlarmsAsync(ct);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var stepAlarms = alarms.Where(a => a.EntityType == AlarmEntityType.ContractStep && a.StepId.HasValue).ToList();
        var stepIds = stepAlarms.Select(a => a.StepId!.Value).Distinct().ToList();
        var contractIds = stepAlarms.Select(a => a.ContractId).Distinct().ToList();

        var openSteps = await _repo.GetOpenStepsAsync(contractIds, stepIds, ct);

        var result = new List<DueAlarmDto>();

        foreach (var alarm in stepAlarms)
        {
            var step = openSteps.FirstOrDefault(x => x.ContractId == alarm.ContractId && x.StepId == alarm.StepId);
            if (step is null || step.DeadLineDate is null) continue;

            var remaining = step.DeadLineDate.Value.DayNumber - today.DayNumber;

            if (remaining <= alarm.DaysBeforeDeadline && remaining >= 0)
            {
                result.Add(new DueAlarmDto
                {
                    AlarmId = alarm.Id,
                    EntityType = alarm.EntityType,
                    ContractId = alarm.ContractId,
                    ContractTitle = step.Contract?.Title,
                    StepId = alarm.StepId,
                    StepTitle = step.Step?.Title,
                    DeadlineDate = step.DeadLineDate.Value,
                    RemainingDays = remaining,
                    DaysBeforeDeadline = alarm.DaysBeforeDeadline
                });
            }
        }

        return result.OrderBy(x => x.RemainingDays).ToList();
    }

    private static DeadlineAlarmDto MapToDto(DeadlineAlarm e) => new()
    {
        Id = e.Id,
        EntityType = e.EntityType,
        ContractId = e.ContractId,
        StepId = e.StepId,
        DaysBeforeDeadline = e.DaysBeforeDeadline,
        IsActive = e.IsActive
    };
}