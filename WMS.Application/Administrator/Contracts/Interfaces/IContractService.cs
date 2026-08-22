// WMS.Application/Administrator/Contracts/Interfaces/IContractService.cs
using WMS.Application.Administrator.ContractBoard.DTOs;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.Contracts.Interfaces;

public interface IContractService
{
    Task<PagedResult<ContractDto>> GetListAsync(ContractFilterRequest filter, CancellationToken ct = default);
    Task<ContractDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractDto> CreateAsync(CreateContractRequest request, CancellationToken ct = default);
    Task<ContractDto> UpdateAsync(Guid id, UpdateContractRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);

    
    Task<ContractBoardResponse> GetBoardAsync(ContractBoardFilterRequest filter, CancellationToken ct = default);
    Task MoveStepAsync(Guid contractId, MoveContractStepRequest request, CancellationToken ct = default);
    
}