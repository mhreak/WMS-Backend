// WMS.Application/Administrator/Contractors/Interfaces/IContractorRepository.cs
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contractors;

namespace WMS.Application.Administrator.Contractors.Interfaces;

public interface IContractorRepository
{
    Task<Contractor?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Contractor?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<Contractor>> GetPagedAsync(ContractorFilterRequest filter, CancellationToken ct = default);
    Task AddAsync(Contractor entity, CancellationToken ct = default);
    Task UpdateAsync(Contractor entity, CancellationToken ct = default);
    Task SoftDeleteAsync(Contractor entity, CancellationToken ct = default);

    Task<bool> ExistsNationalCodeAsync(string nationalCode, Guid? excludeId = null, CancellationToken ct = default);
    Task<bool> ExistsEconomicCodeAsync(string economicCode, Guid? excludeId = null, CancellationToken ct = default);

    // Categories
    Task<List<ContractorCategory>> GetAllCategoriesAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ContractorCategory?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<ContractorCategory>> GetCategoriesByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    Task AddCategoryAsync(ContractorCategory entity, CancellationToken ct = default);
    Task UpdateCategoryAsync(ContractorCategory entity, CancellationToken ct = default);
}