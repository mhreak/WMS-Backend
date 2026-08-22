using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Statements;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Statements;

public class ContractorStatementRepository : IContractorStatementRepository
{
    private readonly AppDbContext _db;
    public ContractorStatementRepository(AppDbContext db) => _db = db;

    public async Task<PagedResult<ContractorStatement>> GetPagedAsync(ContractorStatementFilterRequest filter, CancellationToken ct = default)
    {
        var query = _db.ContractorStatement
            .AsNoTracking()
            .Include(x => x.Contract)
            .Include(x => x.ContractTypeStep)
            .Include(x => x.Attachment)
            .Include(x => x.ExtraOrDeductions)
                .ThenInclude(i => i.Rule)
                    .ThenInclude(r => r.ExtraOrDeductionType)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (filter.ContractId.HasValue)
            query = query.Where(x => x.ContractId == filter.ContractId.Value);

        query = query.OrderByDescending(x => x.StatementDate);

        var pagination = new PaginationQuery { Page = filter.Page, PageSize = filter.PageSize }.Normalize();
        var totalCount = await query.CountAsync(ct);
        var items = await query.Skip(pagination.Skip).Take(pagination.Take).ToListAsync(ct);

        return PagedResult<ContractorStatement>.Create(items, pagination, totalCount);
    }

    public async Task<ContractorStatement?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractorStatement.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<ContractorStatement?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.ContractorStatement
            .Include(x => x.Contract)
            .Include(x => x.ContractTypeStep)
            .Include(x => x.Attachment)
            .Include(x => x.ExtraOrDeductions)
                .ThenInclude(i => i.Rule)
                    .ThenInclude(r => r.ExtraOrDeductionType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task AddAsync(ContractorStatement entity, CancellationToken ct = default)
    {
        await _db.ContractorStatement.AddAsync(entity, ct);
    }

    public Task UpdateAsync(ContractorStatement entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.ContractorStatement.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<List<ExtraOrDeductionRule>> GetRulesByIdsAsync(IEnumerable<Guid> ruleIds, CancellationToken ct = default)
    {
        var idList = ruleIds.Distinct().ToList();
        if (idList.Count == 0) return new List<ExtraOrDeductionRule>();

        return await _db.ExtraOrDeductionRule
            .Include(x => x.ExtraOrDeductionType)
            .Where(x => idList.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task<List<ContractorStatementExtraOrDeduction>> GetItemsByStatementIdAsync(Guid statementId, CancellationToken ct = default)
    {
        return await _db.ContractorStatementExtraOrDeduction
            .Where(x => x.StatementId == statementId && !x.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task ReplaceItemsAsync(Guid statementId, List<ContractorStatementExtraOrDeduction> items, CancellationToken ct = default)
    {
        var existing = await _db.ContractorStatementExtraOrDeduction
            .Where(x => x.StatementId == statementId)
            .ToListAsync(ct);

        _db.ContractorStatementExtraOrDeduction.RemoveRange(existing);

        if (items.Count > 0)
            await _db.ContractorStatementExtraOrDeduction.AddRangeAsync(items, ct);
    }
}