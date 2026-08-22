// WMS.Application/Administrator/ContractTypes/Interfaces/IContractTypeRepository.cs
using WMS.Application.Administrator.ContractTypes.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Application.Administrator.ContractTypes.Interfaces;

public interface IContractTypeRepository
{
    Task<ContractType?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<ContractType>> GetPagedAsync(ContractTypeFilterRequest filter, CancellationToken ct = default);
    Task<List<ContractType>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task AddAsync(ContractType entity, CancellationToken ct = default);
    Task UpdateAsync(ContractType entity, CancellationToken ct = default);
    Task SoftDeleteAsync(ContractType entity, CancellationToken ct = default);
    Task<bool> ExistsTitleAsync(string title, Guid? excludeId = null, CancellationToken ct = default);
}