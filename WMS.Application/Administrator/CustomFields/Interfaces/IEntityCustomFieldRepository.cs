using WMS.Domain.Entities.CustomFields;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.CustomFields.Interfaces;

public interface IEntityCustomFieldRepository
{
    Task<List<EntityCustomField>> GetByEntityTypeAsync(EntityType entityType, CancellationToken ct = default);
    Task<EntityCustomField?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<EntityCustomField_Entity>> GetValuesAsync(Guid entityId, List<Guid> fieldIds, CancellationToken ct = default);
    Task UpsertValueAsync(Guid entityCustomFieldId, Guid entityId, string? value, CancellationToken ct = default);
}