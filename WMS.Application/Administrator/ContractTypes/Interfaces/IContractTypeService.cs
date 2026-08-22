// WMS.Application/Administrator/ContractTypes/Interfaces/IContractTypeService.cs
using WMS.Application.Administrator.ContractTypes.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.ContractTypes.Interfaces;

public interface IContractTypeService
{
    Task<PagedResult<ContractTypeDto>> GetListAsync(ContractTypeFilterRequest filter, CancellationToken ct = default);
    Task<List<ContractTypeDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ContractTypeDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractTypeDto> CreateAsync(CreateContractTypeRequest request, CancellationToken ct = default);
    Task<ContractTypeDto> UpdateAsync(Guid id, UpdateContractTypeRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task ToggleActiveAsync(Guid id, CancellationToken ct = default);
}