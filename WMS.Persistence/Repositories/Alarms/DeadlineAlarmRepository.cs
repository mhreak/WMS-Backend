using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Alarms.Interfaces;
using WMS.Domain.Entities.Alarms;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Statements;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Alarms;

public class DeadlineAlarmRepository : IDeadlineAlarmRepository
{
    private readonly AppDbContext _db;
    public DeadlineAlarmRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(DeadlineAlarm entity, CancellationToken ct = default)
    {
        await _db.DeadlineAlarm.AddAsync(entity, ct);
    }

    public async Task<DeadlineAlarm?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.DeadlineAlarm.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<List<DeadlineAlarm>> GetActiveAlarmsAsync(CancellationToken ct = default)
    {
        return await _db.DeadlineAlarm
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.IsActive)
            .ToListAsync(ct);
    }

    public async Task<List<Contract>> GetContractsByIdsAsync(List<Guid> ids, CancellationToken ct = default)
    {
        if (ids.Count == 0) return new List<Contract>();
        return await _db.Contract
            .Where(x => ids.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task<List<ContractStep>> GetContractStepsAsync(List<(Guid ContractId, Guid StepId)> pairs, CancellationToken ct = default)
    {
        if (pairs.Count == 0) return new List<ContractStep>();

        var contractIds = pairs.Select(p => p.ContractId).Distinct().ToList();
        var stepIds = pairs.Select(p => p.StepId).Distinct().ToList();

        // چون خود جدول Composite Key داره، اول با هر دو لیست فیلتر گسترده می‌کنیم، بعد توی حافظه دقیق می‌کنیم
        var candidates = await _db.Contract_ContractTypeStep
            .Include(x => x.Step)
            .Include(x => x.Contract)
            .Where(x => contractIds.Contains(x.ContractId) && stepIds.Contains(x.StepId))
            .ToListAsync(ct);

        var pairSet = pairs.ToHashSet();
        return candidates.Where(x => pairSet.Contains((x.ContractId, x.StepId))).ToList();

    }
    public async Task<List<ContractorStatement>> GetContractorStatementsByIdsAsync(List<Guid> ids, CancellationToken ct = default)
    {
        if (ids.Count == 0) return new List<ContractorStatement>();
        return await _db.ContractorStatement
            .Include(x => x.Contract)
            .Where(x => ids.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync(ct);
    }
}