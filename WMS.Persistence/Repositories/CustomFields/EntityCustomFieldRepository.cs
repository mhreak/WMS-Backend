using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.CustomFields.Interfaces;
using WMS.Domain.Entities.CustomFields;
using WMS.Domain.Enums;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.CustomFields;

public class EntityCustomFieldRepository : IEntityCustomFieldRepository
{
    private readonly AppDbContext _db;
    public EntityCustomFieldRepository(AppDbContext db) => _db = db;

    public async Task<List<EntityCustomField>> GetByEntityTypeAsync(EntityType entityType, CancellationToken ct = default)
    {
        return await _db.EntityCustomField
            .AsNoTracking()
            .Where(x => x.EntityType == entityType && x.IsActive && !x.IsDeleted)
            .OrderBy(x => x.Form_ShowOrder)
            .ToListAsync(ct);
    }

    public async Task<EntityCustomField?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.EntityCustomField
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<List<EntityCustomField_Entity>> GetValuesAsync(Guid entityId, List<Guid> fieldIds, CancellationToken ct = default)
    {
        return await _db.EntityCustomField_Entity
            .AsNoTracking()
            .Where(x => x.EntityId == entityId && fieldIds.Contains(x.EntityCustomFieldId))
            .ToListAsync(ct);
    }

    public async Task UpsertValueAsync(Guid entityCustomFieldId, Guid entityId, string? value, CancellationToken ct = default)
{
    var existing = await _db.EntityCustomField_Entity
        .FirstOrDefaultAsync(x => x.EntityCustomFieldId == entityCustomFieldId && x.EntityId == entityId, ct);

    if (existing != null)
    {
        existing.Value = value;
    }
    else
    {
        await _db.EntityCustomField_Entity.AddAsync(new EntityCustomField_Entity  { 
            EntityCustomFieldId = entityCustomFieldId,
            EntityId = entityId,
            Value = value
        }, ct);
    }
}
}
