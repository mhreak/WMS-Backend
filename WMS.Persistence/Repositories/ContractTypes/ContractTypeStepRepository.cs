// WMS.Persistence/Repositories/Steps/StepRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.ContractTypeSteps.DTOs;
using WMS.Application.Administrator.ContractTypeSteps.Interfaces;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.ContractTypeSteps;

public class StepRepository : IStepRepository
{
    private readonly AppDbContext _db;

    public StepRepository(AppDbContext db) => _db = db;

    public async Task<ContractTypeStep?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractTypeStep
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<ContractTypeStep?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractTypeStep
            .Include(x => x.ContractType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<PagedResult<ContractTypeStep>> GetPagedAsync(StepFilterRequest filter, CancellationToken ct = default)
    {
        var query = _db.ContractTypeStep
            .AsNoTracking()
            .Include(x => x.ContractType)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(search));
        }

        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive.Value);

        if (filter.ContractTypeId.HasValue)
            query = query.Where(x => x.ContractTypeId == filter.ContractTypeId.Value);

        query = query.OrderBy(x => x.CreatedAt);

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

        return PagedResult<ContractTypeStep>.Create(items, pagination, totalCount);
    }

   public async Task<List<ContractTypeStep>> GetByContractTypeAsync(Guid contractTypeId, bool onlyActive = true, CancellationToken ct = default)
{
    var query = _db.ContractTypeStep
        .AsNoTracking()
        .Where(x => !x.IsDeleted && x.ContractTypeId == contractTypeId);

    if (onlyActive)
        query = query.Where(x => x.IsActive);

    return await query.OrderBy(x => x.StepOrder).ToListAsync(ct);
}

    public async Task AddAsync(ContractTypeStep entity, CancellationToken ct = default)
    {
        await _db.ContractTypeStep.AddAsync(entity, ct);
    }

    public Task UpdateAsync(ContractTypeStep entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractTypeStep.Update(entity);
        return Task.CompletedTask;
    }

    public Task SoftDeleteAsync(ContractTypeStep entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractTypeStep.Update(entity);
        return Task.CompletedTask;
    }
    public async Task<bool> ExistsOrderAsync(Guid contractTypeId, short stepOrder, Guid? excludeId = null, CancellationToken ct = default)
{
    var query = _db.ContractTypeStep
        .Where(x => !x.IsDeleted &&
                    x.ContractTypeId == contractTypeId &&
                    x.StepOrder == stepOrder);

    if (excludeId.HasValue)
        query = query.Where(x => x.Id != excludeId.Value);

    return await query.AnyAsync(ct);
}
}