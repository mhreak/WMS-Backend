using WMS.Domain.Entities.Alarms;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.Alarms.Interfaces;

public interface IDeadlineAlarmRepository
{
    Task AddAsync(DeadlineAlarm entity, CancellationToken ct = default);
    Task<DeadlineAlarm?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<DeadlineAlarm>> GetActiveAlarmsAsync(CancellationToken ct = default);
    Task<List<ContractStep>> GetOpenStepsAsync(List<Guid> contractIds, List<Guid> stepIds, CancellationToken ct = default);
}