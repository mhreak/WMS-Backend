// WMS.Application/Administrator/ContractTypeStates/Interfaces/IContractTypeStateService.cs
using WMS.Application.Administrator.ContractTypeStates.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.ContractTypeStates.Interfaces;

public interface IContractTypeStateService
{
    Task<PagedResult<ContractTypeStateDto>> GetListAsync(ContractTypeStateFilterRequest filter, CancellationToken ct = default);
    Task<List<ContractTypeStateDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ContractTypeStateDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractTypeStateDto> CreateAsync(CreateContractTypeStateRequest request, CancellationToken ct = default);
    Task<ContractTypeStateDto> UpdateAsync(Guid id, UpdateContractTypeStateRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task ToggleActiveAsync(Guid id, CancellationToken ct = default);
}