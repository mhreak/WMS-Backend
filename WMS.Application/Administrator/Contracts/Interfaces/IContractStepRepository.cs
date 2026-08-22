// WMS.Application/Administrator/Contracts/Interfaces/IContractStepRepository.cs
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.Contracts.Interfaces;

public interface IContractStepRepository
{
    Task<ContractStep?> GetAsync(Guid contractId, Guid stepId, CancellationToken ct = default);
    Task<List<ContractStep>> GetByContractIdAsync(Guid contractId, CancellationToken ct = default);
    Task AddAsync(ContractStep entity, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<ContractStep> entities, CancellationToken ct = default);
    Task UpdateAsync(ContractStep entity, CancellationToken ct = default);
    Task RemoveAsync(ContractStep entity, CancellationToken ct = default);
    Task RemoveByContractIdAsync(Guid contractId, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid contractId, Guid stepId, CancellationToken ct = default);
}