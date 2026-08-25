using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Alarms.Interfaces;
using WMS.Domain.Entities.Alarms;
using WMS.Domain.Entities.Contracts;
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
        return await _db.DeadlineAlarm
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<List<DeadlineAlarm>> GetActiveAlarmsAsync(CancellationToken ct = default)
    {
        return await _db.DeadlineAlarm
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.IsActive)
            .ToListAsync(ct);
    }

    public async Task<List<ContractStep>> GetOpenStepsAsync(List<Guid> contractIds, List<Guid> stepIds, CancellationToken ct = default)
    {
        return await _db.Contract_ContractTypeStep
            .Include(x => x.Step)
            .Include(x => x.Contract)
            .Where(x =>
                contractIds.Contains(x.ContractId) &&
                stepIds.Contains(x.StepId) &&
                x.FinishDate == null &&
                x.DeadLineDate != null)
            .ToListAsync(ct);
    }
}