// WMS.Application/Administrator/Steps/Interfaces/IStepService.cs
using WMS.Application.Administrator.ContractTypeSteps.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.Steps.Interfaces;

public interface IStepService
{
    Task<PagedResult<StepDto>> GetListAsync(StepFilterRequest filter, CancellationToken ct = default);
    Task<List<StepDto>> GetByContractTypeAsync(Guid contractTypeId, bool onlyActive = true, CancellationToken ct = default);
    Task<StepDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<StepDto> CreateAsync(CreateStepRequest request, CancellationToken ct = default);
    Task<StepDto> UpdateAsync(Guid id, UpdateStepRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task ToggleActiveAsync(Guid id, CancellationToken ct = default);
}