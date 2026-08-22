// WMS.Application/Administrator/Labels/Interfaces/ILabelRepository.cs
using WMS.Application.Administrator.Labels.DTOs;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Labels.Interfaces;

public interface ILabelRepository
{
    Task<Label?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<Label>> GetPagedAsync(LabelFilterRequest filter, CancellationToken ct = default);
    Task<List<Label>> GetByEntityTypeAsync(EntityType entityType, bool onlyActive = true, CancellationToken ct = default);
    Task AddAsync(Label entity, CancellationToken ct = default);
    Task UpdateAsync(Label entity, CancellationToken ct = default);
    Task SoftDeleteAsync(Label entity, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(string name, EntityType entityType, Guid? excludeId = null, CancellationToken ct = default);

   Task<List<EntityLabel>> GetEntityLabelsAsync(Guid entityId, CancellationToken ct = default);
    Task AddEntityLabelAsync(EntityLabel entityLabel, CancellationToken ct = default);
    Task RemoveEntityLabelsAsync(Guid entityId, CancellationToken ct = default);
    Task RemoveEntityLabelsAsync(Guid entityId, IEnumerable<Guid> labelIds, CancellationToken ct = default);
    Task<bool> ExistsEntityLabelAsync(Guid labelId, Guid entityId, CancellationToken ct = default);
}