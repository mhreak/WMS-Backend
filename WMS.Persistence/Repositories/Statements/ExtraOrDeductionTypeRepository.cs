using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Domain.Entities.Statements;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Statements;

public class ExtraOrDeductionTypeRepository : IExtraOrDeductionTypeRepository
{
    private readonly AppDbContext _db;
    public ExtraOrDeductionTypeRepository(AppDbContext db) => _db = db;

    public async Task<List<ExtraOrDeductionType>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var query = _db.ExtraOrDeductionType.AsNoTracking().Where(x => !x.IsDeleted);
        if (onlyActive) query = query.Where(x => x.IsActive);
        return await query.OrderBy(x => x.Title).ToListAsync(ct);
    }

    public async Task<ExtraOrDeductionType?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ExtraOrDeductionType.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task AddAsync(ExtraOrDeductionType entity, CancellationToken ct = default)
    {
        await _db.ExtraOrDeductionType.AddAsync(entity, ct);
    }

    public Task UpdateAsync(ExtraOrDeductionType entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ExtraOrDeductionType.Update(entity);
        return Task.CompletedTask;
    }
}