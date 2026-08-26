using WMS.Application.Administrator.Alarms.DTOs;
using WMS.Application.Administrator.Alarms.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Alarms;
using WMS.Domain.Enums;

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
    if (request.EntityType == EntityType.ContractStep && !request.StepId.HasValue)
        throw new BadRequestException(MessageKeys.StepIdRequiredForContractStepAlarm);

    ValidateReferenceDateType(request.EntityType, request.ReferenceDateType);

    var entity = new DeadlineAlarm
    {
        Id = Guid.NewGuid(),
        EntityType = request.EntityType,
        EntityId = request.EntityId,
        StepId = request.StepId,
        ReferenceDateType = request.ReferenceDateType,
        DaysOffset = request.DaysOffset,
        IsActive = true,
        CreatedAt = DateTime.UtcNow
    };

    await _repo.AddAsync(entity, ct);
    await _uow.SaveChangesAsync(ct);
    return MapToDto(entity);
}
private static void ValidateReferenceDateType(EntityType entityType, AlarmReferenceDateType referenceDateType)
{
    var allowed = entityType switch
    {
        EntityType.Contract => new[] { AlarmReferenceDateType.StartDate, AlarmReferenceDateType.FinishDate },
        EntityType.ContractStep => new[] { AlarmReferenceDateType.StartDate, AlarmReferenceDateType.FinishDate, AlarmReferenceDateType.DeadlineDate },
        EntityType.ContractorStatement => new[] { AlarmReferenceDateType.StatementDate },
        _ => Array.Empty<AlarmReferenceDateType>()
    };

    if (!allowed.Contains(referenceDateType))
        throw new BadRequestException(MessageKeys.InvalidReferenceDateTypeForEntity);
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
        var result = new List<DueAlarmDto>();

        // ===== Contract =====
        var contractAlarms = alarms.Where(a => a.EntityType == EntityType.Contract).ToList();
        var contractIds = contractAlarms.Select(a => a.EntityId).Distinct().ToList();
        var contracts = await _repo.GetContractsByIdsAsync(contractIds, ct);

        foreach (var alarm in contractAlarms)
        {
            var contract = contracts.FirstOrDefault(c => c.Id == alarm.EntityId);
            if (contract is null) continue;

            var (referenceDate, isOpen) = alarm.ReferenceDateType switch
            {
                AlarmReferenceDateType.StartDate => (contract.StartDate, contract.FinishedDate == null),
                AlarmReferenceDateType.FinishDate => (contract.FinishedDate, contract.FinishedDate != null),
                _ => ((DateOnly?)null, false)
            };

            TryAddDue(result, alarm, referenceDate, isOpen, today, contract.Title, null);
        }

        // ===== ContractStep =====
        var stepAlarms = alarms.Where(a => a.EntityType == EntityType.ContractStep && a.StepId.HasValue).ToList();
        var pairs = stepAlarms.Select(a => (a.EntityId, a.StepId!.Value)).Distinct().ToList();
        var steps = await _repo.GetContractStepsAsync(pairs, ct);

        foreach (var alarm in stepAlarms)
        {
            var step = steps.FirstOrDefault(s => s.ContractId == alarm.EntityId && s.StepId == alarm.StepId);
            if (step is null) continue;

            var (referenceDate, isOpen) = alarm.ReferenceDateType switch
            {
                AlarmReferenceDateType.StartDate => (step.StartDate, step.FinishDate == null),
                AlarmReferenceDateType.FinishDate => (step.FinishDate, step.FinishDate != null),
                _ => ((DateOnly?)null, false)
            };

            var title = $"{step.Contract?.Title} / {step.Step?.Title}";
            TryAddDue(result, alarm, referenceDate, isOpen, today, title, alarm.StepId);
        }
        // ===== ContractorStatement =====
        var statementAlarms = alarms.Where(a => a.EntityType == EntityType.ContractorStatement).ToList();
        var statementIds = statementAlarms.Select(a => a.EntityId).Distinct().ToList();
        var statements = await _repo.GetContractorStatementsByIdsAsync(statementIds, ct);

        foreach (var alarm in statementAlarms)
        {
            var statement = statements.FirstOrDefault(s => s.Id == alarm.EntityId);
            if (statement is null) continue;

            // صورت‌وضعیت مفهوم "باز/بسته" نداره (بر خلاف Contract/ContractStep)، پس همیشه isOpen = true
            var referenceDate = alarm.ReferenceDateType == AlarmReferenceDateType.StatementDate
                ? (DateOnly?)statement.StatementDate
                : null;

            var title = $"{statement.Contract?.Title} / {statement.Title}";
            TryAddDue(result, alarm, referenceDate, isOpen: true, today, title, null);
        }


        return result.OrderBy(x => x.RemainingDays).ToList();
    }

    private static void TryAddDue(
        List<DueAlarmDto> result,
        DeadlineAlarm alarm,
        DateOnly? referenceDate,
        bool isOpen,
        DateOnly today,
        string? title,
        Guid? stepId)
    {
        if (!isOpen || referenceDate is null) return;

        var targetDate = referenceDate.Value.AddDays(alarm.DaysOffset);
        var remaining = targetDate.DayNumber - today.DayNumber;

        if (remaining <= 0)
        {
            result.Add(new DueAlarmDto
            {
                AlarmId = alarm.Id,
                EntityType = alarm.EntityType,
                EntityId = alarm.EntityId,
                StepId = stepId,
                EntityTitle = title,
                ReferenceDateType = alarm.ReferenceDateType,
                ReferenceDate = referenceDate.Value,
                TargetDate = targetDate,
                RemainingDays = remaining
            });
        }
    }

    private static DeadlineAlarmDto MapToDto(DeadlineAlarm e) => new()
    {
        Id = e.Id,
        EntityType = e.EntityType,
        EntityId = e.EntityId,
        StepId = e.StepId,
        ReferenceDateType = e.ReferenceDateType,
        DaysOffset = e.DaysOffset,
        IsActive = e.IsActive
    };
}