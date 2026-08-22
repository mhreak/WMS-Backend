// IAttachmentTypeRepository.cs
using WMS.Domain.Entities.Attachments;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Attachments.Interfaces;

public interface IAttachmentTypeRepository
{
    Task<AttachmentType?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<AttachmentType>> GetAllAsync(EntityType? entityType = null, bool onlyActive = true, CancellationToken ct = default);
    Task AddAsync(AttachmentType entity, CancellationToken ct = default);
    Task UpdateAsync(AttachmentType entity, CancellationToken ct = default);
    Task SoftDeleteAsync(AttachmentType entity, CancellationToken ct = default);
    Task<bool> ExistsTitleAsync(string title, EntityType entityType, Guid? excludeId = null, CancellationToken ct = default);
}