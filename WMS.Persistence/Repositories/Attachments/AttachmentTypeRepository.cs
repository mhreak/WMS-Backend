// AttachmentTypeRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Domain.Entities.Attachments;
using WMS.Domain.Enums;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Attachments;

public class AttachmentTypeRepository : IAttachmentTypeRepository
{
    private readonly AppDbContext _db;
    public AttachmentTypeRepository(AppDbContext db) => _db = db;

    public async Task<AttachmentType?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.AttachmentTypes.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);

    public async Task<List<AttachmentType>> GetAllAsync(EntityType? entityType = null, bool onlyActive = true, CancellationToken ct = default)
    {
        var query = _db.AttachmentTypes.AsNoTracking().Where(x => !x.IsDeleted);
        if (entityType.HasValue) query = query.Where(x => x.EntityType == entityType.Value);
        if (onlyActive) query = query.Where(x => x.IsActive);
        return await query.OrderBy(x => x.Title).ToListAsync(ct);
    }

    public async Task AddAsync(AttachmentType entity, CancellationToken ct = default)
        => await _db.AttachmentTypes.AddAsync(entity, ct);

    public Task UpdateAsync(AttachmentType entity, CancellationToken ct = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _db.AttachmentTypes.Update(entity);
        return Task.CompletedTask;
    }

    public Task SoftDeleteAsync(AttachmentType entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.AttachmentTypes.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsTitleAsync(string title, EntityType entityType, Guid? excludeId = null, CancellationToken ct = default)
    {
        var query = _db.AttachmentTypes.Where(x =>
            !x.IsDeleted &&
            x.EntityType == entityType &&
            x.Title.ToLower() == title.ToLower());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }
}