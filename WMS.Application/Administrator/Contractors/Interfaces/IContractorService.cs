// WMS.Application/Administrator/Contractors/Interfaces/IContractorService.cs
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Common.Pagination;

namespace WMS.Application.Administrator.Contractors.Interfaces;

public interface IContractorService
{
    Task<PagedResult<ContractorListItemDto>> GetListAsync(ContractorFilterRequest filter, CancellationToken ct = default);
    Task<ContractorDetailDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ContractorDetailDto> CreateAsync(CreateContractorRequest request, CancellationToken ct = default);
    Task<ContractorDetailDto> UpdateAsync(Guid id, UpdateContractorRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task ToggleActiveAsync(Guid id, CancellationToken ct = default);

    // Categories
    Task<List<ContractorCategoryDto>> GetCategoriesAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ContractorCategoryDto> CreateCategoryAsync(CreateContractorCategoryRequest request, CancellationToken ct = default);
    Task<ContractorCategoryDto> UpdateCategoryAsync(Guid id, UpdateContractorCategoryRequest request, CancellationToken ct = default);
    Task DeleteCategoryAsync(Guid id, CancellationToken ct = default);
}