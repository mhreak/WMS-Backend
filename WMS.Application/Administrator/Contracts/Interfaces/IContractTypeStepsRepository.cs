// WMS.Application/Administrator/Steps/Interfaces/IStepRepository.cs
using WMS.Application.Administrator.ContractTypeSteps.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.ContractTypeSteps.Interfaces;

public interface IStepRepository
{
    Task<ContractTypeStep?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractTypeStep?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<ContractTypeStep>> GetPagedAsync(StepFilterRequest filter, CancellationToken ct = default);
    Task<List<ContractTypeStep>> GetByContractTypeAsync(Guid contractTypeId, bool onlyActive = true, CancellationToken ct = default);
    Task AddAsync(ContractTypeStep entity, CancellationToken ct = default);
    Task UpdateAsync(ContractTypeStep entity, CancellationToken ct = default);
    Task SoftDeleteAsync(ContractTypeStep entity, CancellationToken ct = default);
    Task<bool> ExistsOrderAsync(Guid contractTypeId, short stepOrder, Guid? excludeId = null, CancellationToken ct = default);
}