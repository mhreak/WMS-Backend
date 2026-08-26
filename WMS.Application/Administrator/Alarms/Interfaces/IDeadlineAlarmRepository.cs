using WMS.Domain.Entities.Alarms;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Alarms.Interfaces;

public interface IDeadlineAlarmRepository
{
    Task AddAsync(DeadlineAlarm entity, CancellationToken ct = default);
    Task<DeadlineAlarm?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<DeadlineAlarm>> GetActiveAlarmsAsync(CancellationToken ct = default);

    Task<List<Contract>> GetContractsByIdsAsync(List<Guid> ids, CancellationToken ct = default);

    // برای EntityType=ContractStep: (ContractId, StepId) جفت‌جفت پاس داده می‌شه
    Task<List<ContractStep>> GetContractStepsAsync(List<(Guid ContractId, Guid StepId)> pairs, CancellationToken ct = default);

    Task<List<ContractorStatement>> GetContractorStatementsByIdsAsync(List<Guid> ids, CancellationToken ct = default);
}