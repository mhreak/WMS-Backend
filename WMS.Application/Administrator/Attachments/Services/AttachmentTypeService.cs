// WMS.Application/Administrator/Attachments/Services/AttachmentTypeService.cs
using WMS.Application.Administrator.Attachments.DTOs;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Attachments;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Attachments.Services;

public class AttachmentTypeService : IAttachmentTypeService
{
    private readonly IAttachmentTypeRepository _repo;
    private readonly IUnitOfWork _uow;

    public AttachmentTypeService(IAttachmentTypeRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<List<AttachmentTypeDto>> GetAllAsync(
        EntityType? entityType = null,
        bool onlyActive = true,
        CancellationToken ct = default)
    {
        var list = await _repo.GetAllAsync(entityType, onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<AttachmentTypeDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentTypeNotFound);

        return MapToDto(entity);
    }

    public async Task<AttachmentTypeDto> CreateAsync(CreateAttachmentTypeRequest request, CancellationToken ct = default)
    {
        var title = request.Title.Trim();

        if (await _repo.ExistsTitleAsync(title, request.EntityType, null, ct))
            throw new BadRequestException(MessageKeys.AttachmentTypeTitleAlreadyExists);

        var entity = new AttachmentType
        {
            Id = Guid.NewGuid(),
            EntityType = request.EntityType,
            Title = title,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task<AttachmentTypeDto> UpdateAsync(Guid id, UpdateAttachmentTypeRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentTypeNotFound);

        var title = request.Title.Trim();

        if (await _repo.ExistsTitleAsync(title, request.EntityType, id, ct))
            throw new BadRequestException(MessageKeys.AttachmentTypeTitleAlreadyExists);

        entity.EntityType = request.EntityType;
        entity.Title = title;
        entity.IsActive = request.IsActive;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentTypeNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ToggleActiveAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.AttachmentTypeNotFound);

        entity.IsActive = !entity.IsActive;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private static string GetEntityTypeName(EntityType type) => type switch
    {
        EntityType.Contractor => "پیمانکار",
        EntityType.Contract => "قرارداد",
        _ => type.ToString()
    };

    private static AttachmentTypeDto MapToDto(AttachmentType entity) => new()
    {
        Id = entity.Id,
        EntityType = entity.EntityType,
        EntityTypeName = GetEntityTypeName(entity.EntityType),
        Title = entity.Title,
        IsActive = entity.IsActive,
        CreatedAt = entity.CreatedAt
    };
}