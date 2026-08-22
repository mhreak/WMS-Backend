// WMS.Application/Administrator/Labels/Interfaces/ILabelService.cs
using WMS.Application.Administrator.Labels.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Labels.Interfaces;

public interface ILabelService
{
    Task<PagedResult<LabelDto>> GetListAsync(LabelFilterRequest filter, CancellationToken ct = default);
    Task<LabelDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<LabelDto>> GetByEntityTypeAsync(EntityType entityType, bool onlyActive = true, CancellationToken ct = default);
    Task<LabelDto> CreateAsync(CreateLabelRequest request, CancellationToken ct = default);
    Task<LabelDto> UpdateAsync(Guid id, UpdateLabelRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task ToggleActiveAsync(Guid id, CancellationToken ct = default);
}