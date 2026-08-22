using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Statements.Interfaces;

public interface IContractorStatementRepository
{
    Task<PagedResult<ContractorStatement>> GetPagedAsync(ContractorStatementFilterRequest filter, CancellationToken ct = default);
    Task<ContractorStatement?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractorStatement?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(ContractorStatement entity, CancellationToken ct = default);
    Task UpdateAsync(ContractorStatement entity, CancellationToken ct = default);

    // Extra/Deduction items
    Task<List<ExtraOrDeductionRule>> GetRulesByIdsAsync(IEnumerable<Guid> ruleIds, CancellationToken ct = default);
    Task<List<ContractorStatementExtraOrDeduction>> GetItemsByStatementIdAsync(Guid statementId, CancellationToken ct = default);
    Task ReplaceItemsAsync(Guid statementId, List<ContractorStatementExtraOrDeduction> items, CancellationToken ct = default);
}