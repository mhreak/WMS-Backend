using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Statements;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Statements;

public class ExtraOrDeductionRuleRepository : IExtraOrDeductionRuleRepository
{
    private readonly AppDbContext _db;
    public ExtraOrDeductionRuleRepository(AppDbContext db) => _db = db;

   public async Task<PagedResult<ExtraOrDeductionRule>> GetPagedAsync(
    ExtraOrDeductionRuleFilterRequest filter, CancellationToken ct = default)
{
    var query = _db.ExtraOrDeductionRule
        .AsNoTracking()
        .Include(x => x.ContractType)
        .Include(x => x.ExtraOrDeductionType)
        .Include(x => x.ContractorCategory)
        .Include(x => x.ContractCategory)
        .Include(x => x.Contractor)
        .Include(x => x.ContractLabel)
        .Where(x => !x.IsDeleted)
        .AsQueryable();

    if (filter.ContractTypeId.HasValue)
        query = query.Where(x => x.ContractTypeId == filter.ContractTypeId.Value);

    if (filter.ExtraOrDeductionTypeId.HasValue)
        query = query.Where(x => x.ExtraOrDeductionTypeId == filter.ExtraOrDeductionTypeId.Value);

    if (filter.ContractorCategoryId.HasValue)
        query = query.Where(x => x.ContractorCategoryId == filter.ContractorCategoryId.Value);

    if (filter.ContractCategoryId.HasValue)
        query = query.Where(x => x.ContractCategoryId == filter.ContractCategoryId.Value);

    if (filter.ContractLabelId.HasValue)
        query = query.Where(x => x.ContractLabelId == filter.ContractLabelId.Value);

    query = query.OrderByDescending(x => x.CreatedAt);

    var pagination = new PaginationQuery { Page = filter.Page, PageSize = filter.PageSize }.Normalize();
    var totalCount = await query.CountAsync(ct);
    var items = await query.Skip(pagination.Skip).Take(pagination.Take).ToListAsync(ct);

    return PagedResult<ExtraOrDeductionRule>.Create(items, pagination, totalCount);
}

    public async Task<ExtraOrDeductionRule?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ExtraOrDeductionRule.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

   public async Task<ExtraOrDeductionRule?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
{
    return await _db.ExtraOrDeductionRule
        .Include(x => x.ContractType)
        .Include(x => x.ExtraOrDeductionType)
        .Include(x => x.ContractorCategory)
        .Include(x => x.ContractCategory)
        .Include(x => x.Contractor)
        .Include(x => x.ContractLabel)
        .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
}

    public async Task AddAsync(ExtraOrDeductionRule entity, CancellationToken ct = default)
    {
        await _db.ExtraOrDeductionRule.AddAsync(entity, ct);
    }

    public Task UpdateAsync(ExtraOrDeductionRule entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ExtraOrDeductionRule.Update(entity);
        return Task.CompletedTask;
    }
}