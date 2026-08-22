// EntityAttachmentRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Domain.Entities.Attachments;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Attachments;

public class EntityAttachmentRepository : IEntityAttachmentRepository
{
    private readonly AppDbContext _db;
    public EntityAttachmentRepository(AppDbContext db) => _db = db;

    public async Task<EntityAttachment?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.EntityAttachments
            .Include(x => x.AttachmentType)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);

    public async Task<List<EntityAttachment>> GetByEntityIdAsync(Guid entityId, Guid? attachmentTypeId = null, CancellationToken ct = default)
    {
        var query = _db.EntityAttachments
            .AsNoTracking()
            .Include(x => x.AttachmentType)
            .Where(x => x.EntityId == entityId && !x.IsDeleted);

        if (attachmentTypeId.HasValue)
            query = query.Where(x => x.AttachmentTypeId == attachmentTypeId.Value);

        return await query.OrderByDescending(x => x.CreatedAt).ToListAsync(ct);
    }

    public async Task AddAsync(EntityAttachment entity, CancellationToken ct = default)
        => await _db.EntityAttachments.AddAsync(entity, ct);

    public Task SoftDeleteAsync(EntityAttachment entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.EntityAttachments.Update(entity);
        return Task.CompletedTask;
    }
}