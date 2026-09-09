using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.Statements.Interfaces;

public interface IContractorStatementService
{
    Task<PagedResult<ContractorStatementDto>> GetListAsync(ContractorStatementFilterRequest filter, CancellationToken ct = default);
    Task<ContractorStatementDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractorStatementDto> CreateAsync(CreateContractorStatementRequest request, CancellationToken ct = default);
    Task<ContractorStatementDto> UpdateAsync(Guid id, UpdateContractorStatementRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    
}