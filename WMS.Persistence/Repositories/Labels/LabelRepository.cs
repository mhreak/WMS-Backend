// WMS.Persistence/Repositories/Labels/LabelRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Labels.DTOs;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Labels;

public class LabelRepository : ILabelRepository
{
    private readonly AppDbContext _db;

    public LabelRepository(AppDbContext db) => _db = db;

    public async Task<Label?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Labels
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<PagedResult<Label>> GetPagedAsync(LabelFilterRequest filter, CancellationToken ct = default)
    {
        var query = _db.Labels
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(search));
        }

        // if (filter.EntityType.HasValue)
        //     query = query.Where(x => x.EntityType == filter.EntityType.Value);

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

        return PagedResult<Label>.Create(items, pagination, totalCount);
    }

    public async Task<List<Label>> GetByEntityTypeAsync(EntityType entityType, bool onlyActive = true, CancellationToken ct = default)
    {
        var query = _db.Labels
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.EntityType == entityType);

        if (onlyActive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Label entity, CancellationToken ct = default)
    {
        await _db.Labels.AddAsync(entity, ct);
    }

    public Task UpdateAsync(Label entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.Labels.Update(entity);
        return Task.CompletedTask;
    }

    public Task SoftDeleteAsync(Label entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.Labels.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsNameAsync(string name, EntityType entityType, Guid? excludeId = null, CancellationToken ct = default)
    {
        var query = _db.Labels
            .Where(x => !x.IsDeleted &&
                        x.EntityType == entityType &&
                        x.Name.ToLower() == name.ToLower());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }

    // ========== EntityLabel ==========

    public async Task<List<EntityLabel>> GetEntityLabelsAsync(Guid entityId, CancellationToken ct = default)
{
    return await _db.EntityLabels
        .AsNoTracking()
        .Include(x => x.Label)
        .Where(x => x.EntityId == entityId)
        .ToListAsync(ct);
}

public async Task AddEntityLabelAsync(EntityLabel entityLabel, CancellationToken ct = default)
{
    await _db.EntityLabels.AddAsync(entityLabel, ct);
}

public async Task RemoveEntityLabelsAsync(Guid entityId, CancellationToken ct = default)
{
    var items = await _db.EntityLabels
        .Where(x => x.EntityId == entityId)
        .ToListAsync(ct);

    _db.EntityLabels.RemoveRange(items);
}

public async Task RemoveEntityLabelsAsync(Guid entityId, IEnumerable<Guid> labelIds, CancellationToken ct = default)
{
    var idList = labelIds.ToList();
    if (idList.Count == 0) return;

    var items = await _db.EntityLabels
        .Where(x => x.EntityId == entityId && idList.Contains(x.LabelId))
        .ToListAsync(ct);

    _db.EntityLabels.RemoveRange(items);
}

public async Task<bool> ExistsEntityLabelAsync(Guid labelId, Guid entityId, CancellationToken ct = default)
{
    return await _db.EntityLabels
        .AnyAsync(x => x.LabelId == labelId && x.EntityId == entityId, ct);
}
}