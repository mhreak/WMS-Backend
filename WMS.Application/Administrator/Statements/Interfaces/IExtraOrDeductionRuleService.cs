using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.Statements.Interfaces;

public interface IExtraOrDeductionRuleService
{
    Task<PagedResult<ExtraOrDeductionRuleDto>> GetListAsync(ExtraOrDeductionRuleFilterRequest filter, CancellationToken ct = default);
    Task<ExtraOrDeductionRuleDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ExtraOrDeductionRuleDto> CreateAsync(CreateExtraOrDeductionRuleRequest request, CancellationToken ct = default);
    Task<ExtraOrDeductionRuleDto> UpdateAsync(Guid id, UpdateExtraOrDeductionRuleRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}