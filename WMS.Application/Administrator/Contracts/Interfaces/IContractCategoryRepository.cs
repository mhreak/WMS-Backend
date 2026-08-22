
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.Contracts.Interfaces;

public interface IContractCategoryRepository
{
    Task<List<ContractCategory>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ContractCategory?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<ContractCategory>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    Task AddAsync(ContractCategory entity, CancellationToken ct = default);
    Task UpdateAsync(ContractCategory entity, CancellationToken ct = default);
}