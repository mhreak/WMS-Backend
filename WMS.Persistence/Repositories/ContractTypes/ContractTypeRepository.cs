// WMS.Persistence/Repositories/ContractTypes/ContractTypeRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.ContractTypes.DTOs;
using WMS.Application.Administrator.ContractTypes.Interfaces;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.ContractTypes;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.ContractTypes;

public class ContractTypeRepository : IContractTypeRepository
{
    private readonly AppDbContext _db;

    public ContractTypeRepository(AppDbContext db) => _db = db;

    public async Task<ContractType?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractTypes
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<PagedResult<ContractType>> GetPagedAsync(ContractTypeFilterRequest filter, CancellationToken ct = default)
    {
        var query = _db.ContractTypes
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(search));
        }

        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive.Value);

        query = query.OrderByDescending(x => x.CreatedAt);

        var pagination = new PaginationQuery
        {
            Page = filter.Page,
            PageSize = filter.PageSize
        }.Normalize();

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync(ct);

        return PagedResult<ContractType>.Create(items, pagination, totalCount);
    }

    public async Task<List<ContractType>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var query = _db.ContractTypes
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (onlyActive)
            query = query.Where(x => x.IsActive);

        return await query.OrderBy(x => x.Title).ToListAsync(ct);
    }

    public async Task AddAsync(ContractType entity, CancellationToken ct = default)
    {
        await _db.ContractTypes.AddAsync(entity, ct);
    }

    public Task UpdateAsync(ContractType entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractTypes.Update(entity);
        return Task.CompletedTask;
    }

    public Task SoftDeleteAsync(ContractType entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractTypes.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsTitleAsync(string title, Guid? excludeId = null, CancellationToken ct = default)
    {
        var query = _db.ContractTypes
            .Where(x => !x.IsDeleted && x.Title.ToLower() == title.ToLower());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }
}