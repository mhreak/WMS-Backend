// IContractCategoryService.cs
using WMS.Application.Administrator.ContractCategories.DTOs;

public interface IContractCategoryService
{
    Task<List<ContractCategoryDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ContractCategoryDto> CreateAsync(CreateContractCategoryRequest request, CancellationToken ct = default);
    Task<ContractCategoryDto> UpdateAsync(Guid id, UpdateContractCategoryRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}