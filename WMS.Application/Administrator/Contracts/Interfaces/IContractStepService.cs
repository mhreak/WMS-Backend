// WMS.Application/Administrator/Contracts/Interfaces/IContractStepService.cs
using WMS.Application.Administrator.Contracts.DTOs;

namespace WMS.Application.Administrator.Contracts.Interfaces;

public interface IContractStepService
{
    Task<List<ContractStepDto>> GetByContractIdAsync(Guid contractId, CancellationToken ct = default);
    Task<ContractStepDto> CreateAsync(CreateContractStepRequest request, CancellationToken ct = default);
    Task<ContractStepDto> UpdateAsync(Guid contractId, Guid stepId, UpdateContractStepRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid contractId, Guid stepId, CancellationToken ct = default);
    Task<List<ContractStepDto>> SetStepsAsync(SetContractStepsRequest request, CancellationToken ct = default);
}