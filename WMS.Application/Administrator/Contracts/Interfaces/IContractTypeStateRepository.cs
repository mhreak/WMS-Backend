// WMS.Application/Administrator/ContractTypeStates/Interfaces/IContractTypeStateRepository.cs
using WMS.Application.Administrator.ContractTypeStates.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.ContractTypeStates.Interfaces;

public interface IContractTypeStateRepository
{
    Task<ContractTypeState?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<ContractTypeState>> GetPagedAsync(ContractTypeStateFilterRequest filter, CancellationToken ct = default);
    Task<List<ContractTypeState>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task AddAsync(ContractTypeState entity, CancellationToken ct = default);
    Task UpdateAsync(ContractTypeState entity, CancellationToken ct = default);
    Task SoftDeleteAsync(ContractTypeState entity, CancellationToken ct = default);
    Task<bool> ExistsTitleAsync(string title, Guid? excludeId = null, CancellationToken ct = default);
}