// IEntityAttachmentRepository.cs
using WMS.Domain.Entities.Attachments;

namespace WMS.Application.Administrator.Attachments.Interfaces;

public interface IEntityAttachmentRepository
{
    Task<EntityAttachment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<EntityAttachment>> GetByEntityIdAsync(Guid entityId, Guid? attachmentTypeId = null, CancellationToken ct = default);
    Task AddAsync(EntityAttachment entity, CancellationToken ct = default);
    Task SoftDeleteAsync(EntityAttachment entity, CancellationToken ct = default);
}