// WMS.Application/Administrator/Contracts/Interfaces/IContractRepository.cs
using WMS.Application.Administrator.ContractBoard.DTOs;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.Contracts.Interfaces;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Contract?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<Contract>> GetPagedAsync(ContractFilterRequest filter, CancellationToken ct = default);
    Task AddAsync(Contract entity, CancellationToken ct = default);
    Task UpdateAsync(Contract entity, CancellationToken ct = default);
    Task SoftDeleteAsync(Contract entity, CancellationToken ct = default);
    Task<bool> ExistsContractNumberAsync(string contractNumber, Guid? excludeId = null, CancellationToken ct = default);
    Task<ContractBoardResponse> GetBoardAsync(ContractBoardFilterRequest filter, CancellationToken ct = default);
    Task MoveStepAsync(Guid contractId, MoveContractStepRequest request, CancellationToken ct = default);
}