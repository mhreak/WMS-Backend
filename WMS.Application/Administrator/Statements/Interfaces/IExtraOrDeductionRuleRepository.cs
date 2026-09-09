using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Statements.Interfaces;

public interface IExtraOrDeductionRuleRepository
{
    Task<PagedResult<ExtraOrDeductionRule>> GetPagedAsync(ExtraOrDeductionRuleFilterRequest filter, CancellationToken ct = default);
    Task<ExtraOrDeductionRule?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ExtraOrDeductionRule?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(ExtraOrDeductionRule entity, CancellationToken ct = default);
    Task UpdateAsync(ExtraOrDeductionRule entity, CancellationToken ct = default);
    Task<List<ExtraOrDeductionRule>> GetActiveRulesByContractTypeAsync(Guid contractTypeId, CancellationToken ct = default);
}