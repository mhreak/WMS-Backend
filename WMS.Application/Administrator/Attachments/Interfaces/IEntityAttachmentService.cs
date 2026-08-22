// IEntityAttachmentService.cs
using WMS.Application.Administrator.Attachments.DTOs;

namespace WMS.Application.Administrator.Attachments.Interfaces;

public interface IEntityAttachmentService
{
    Task<EntityAttachmentDto> UploadAsync(UploadEntityAttachmentRequest request, Guid currentUserId, CancellationToken ct = default);
    Task<List<EntityAttachmentDto>> GetByEntityIdAsync(Guid entityId, Guid? attachmentTypeId = null, CancellationToken ct = default);
    Task<EntityAttachmentDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}