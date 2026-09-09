// WMS.Persistence/Repositories/Notifications/NotificationRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Notifications.Interfaces;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Notifications;
using WMS.Domain.Entities.Statements;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Notifications;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _db;

    public NotificationRepository(AppDbContext db) => _db = db;

    public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Notifications
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<List<Notification>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default)
    {
        var query = _db.Notifications
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (onlyActive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<List<Notification>> GetActiveAsync(CancellationToken ct = default)
    {
        return await _db.Notifications
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.IsActive)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Notification entity, CancellationToken ct = default)
    {
        await _db.Notifications.AddAsync(entity, ct);
    }

    public Task UpdateAsync(Notification entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        var entry = _db.Entry(entity);
        if (entry.State == EntityState.Detached)
            _db.Notifications.Update(entity);

        return Task.CompletedTask;
    }

    public async Task<List<Contract>> GetContractsAsync(CancellationToken ct = default)
    {
        return await _db.Contract
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task<List<ContractStep>> GetOpenContractStepsAsync(CancellationToken ct = default)
    {
        return await _db.Contract_ContractTypeStep
            .AsNoTracking()
            .Include(x => x.Contract)
            .Include(x => x.Step)
            .Where(x => x.FinishDate == null && x.Contract != null && !x.Contract.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task<List<ContractorStatement>> GetStatementsAsync(CancellationToken ct = default)
    {
        return await _db.ContractorStatement
            .AsNoTracking()
            .Include(x => x.Contract)
            .Where(x => !x.IsDeleted)
            .ToListAsync(ct);
    }
}