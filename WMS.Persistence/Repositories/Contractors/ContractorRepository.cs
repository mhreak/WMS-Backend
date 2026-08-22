// WMS.Persistence/Repositories/Contractors/ContractorRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Administrator.Contractors.Interfaces;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contractors;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Contractors;

public class ContractorRepository : IContractorRepository
{
    private readonly AppDbContext _db;

    public ContractorRepository(AppDbContext db) => _db = db;

    public async Task<Contractor?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Contractors
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<Contractor?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Contractors
            .Include(x => x.City)
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<PagedResult<Contractor>> GetPagedAsync(ContractorFilterRequest filter, CancellationToken ct = default)
    {
        var query = _db.Contractors
            .AsNoTracking()
            .Include(x => x.City)
            .Include(x => x.Categories)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLower();
            query = query.Where(x =>
                (x.FirstName != null && x.FirstName.ToLower().Contains(search)) ||
                (x.LastName != null && x.LastName.ToLower().Contains(search)) ||
                (x.CompanyName != null && x.CompanyName.ToLower().Contains(search)) ||
                (x.Mobile1 != null && x.Mobile1.Contains(search)) ||
                (x.Mobile2 != null && x.Mobile2.Contains(search)) ||
                (x.NationalCode != null && x.NationalCode.Contains(search)) ||
                (x.EconomicCode != null && x.EconomicCode.Contains(search)) ||
                (x.Email != null && x.Email.ToLower().Contains(search)));
        }

        if (filter.Type.HasValue)
            query = query.Where(x => x.Type == filter.Type.Value);

        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive.Value);

        if (filter.ProvinceId.HasValue)
            query = query.Where(x => x.City != null && x.City.ProvinceId == filter.ProvinceId.Value);

        if (filter.CityId.HasValue)
            query = query.Where(x => x.CityId == filter.CityId.Value);

        if (filter.CategoryIds is { Count: > 0 })
            query = query.Where(x => x.Categories.Any(c => filter.CategoryIds.Contains(c.Id)));

        if (filter.LabelIds is { Count: > 0 })
        {
            query = query.Where(x =>
                _db.EntityLabels.Any(el =>
                    el.EntityId == x.Id &&
                    filter.LabelIds.Contains(el.LabelId)));
        }

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

        return PagedResult<Contractor>.Create(items, pagination, totalCount);
    }

    public async Task AddAsync(Contractor entity, CancellationToken ct = default)
    {
        await _db.Contractors.AddAsync(entity, ct);
    }

    public Task UpdateAsync(Contractor entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.Contractors.Update(entity);
        return Task.CompletedTask;
    }

    public Task SoftDeleteAsync(Contractor entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.Contractors.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsNationalCodeAsync(string nationalCode, Guid? excludeId = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(nationalCode)) return false;

        var query = _db.Contractors.Where(x => !x.IsDeleted && x.NationalCode == nationalCode);
        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }

    public async Task<bool> ExistsEconomicCodeAsync(string economicCode, Guid? excludeId = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(economicCode)) return false;

        var query = _db.Contractors.Where(x => !x.IsDeleted && x.EconomicCode == economicCode);
        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }

    // ========== Categories ==========

    public async Task<List<ContractorCategory>> GetAllCategoriesAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var query = _db.ContractorCategories
            .AsNoTracking()
            .Include(x => x.Parent)
            .Where(x => !x.IsDeleted);
        if (onlyActive) query = query.Where(x => x.IsActive);

        return await query.OrderBy(x => x.Name).ToListAsync(ct);
    }

    public async Task<ContractorCategory?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractorCategories
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<List<ContractorCategory>> GetCategoriesByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return new List<ContractorCategory>();

        return await _db.ContractorCategories
            .Where(x => idList.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task AddCategoryAsync(ContractorCategory entity, CancellationToken ct = default)
    {
        await _db.ContractorCategories.AddAsync(entity, ct);
    }

    public Task UpdateCategoryAsync(ContractorCategory entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractorCategories.Update(entity);
        return Task.CompletedTask;
    }
}