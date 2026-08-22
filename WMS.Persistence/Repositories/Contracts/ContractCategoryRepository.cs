// ContractCategoryRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Domain.Entities.Contracts;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Contracts;

public class ContractCategoryRepository : IContractCategoryRepository
{
    private readonly AppDbContext _db;

    public ContractCategoryRepository(AppDbContext db) => _db = db;

    public async Task<List<ContractCategory>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var query = _db.ContractCategory
            .AsNoTracking()
            .Include(x => x.Parent)
            .Where(x => !x.IsDeleted);
        if (onlyActive) query = query.Where(x => x.IsActive);
        return await query.OrderBy(x => x.Name).ToListAsync(ct);
    }

    public async Task<ContractCategory?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractCategory
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<List<ContractCategory>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return new List<ContractCategory>();

        return await _db.ContractCategory
            .Where(x => idList.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task AddAsync(ContractCategory entity, CancellationToken ct = default)
    {
        await _db.ContractCategory.AddAsync(entity, ct);
    }

    public Task UpdateAsync(ContractCategory entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractCategory.Update(entity);
        return Task.CompletedTask;
    }
}