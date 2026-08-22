// IAttachmentTypeService.cs
using WMS.Application.Administrator.Attachments.DTOs;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Attachments.Interfaces;

public interface IAttachmentTypeService
{
    Task<List<AttachmentTypeDto>> GetAllAsync(EntityType? entityType = null, bool onlyActive = true, CancellationToken ct = default);
    Task<AttachmentTypeDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AttachmentTypeDto> CreateAsync(CreateAttachmentTypeRequest request, CancellationToken ct = default);
    Task<AttachmentTypeDto> UpdateAsync(Guid id, UpdateAttachmentTypeRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task ToggleActiveAsync(Guid id, CancellationToken ct = default);
}