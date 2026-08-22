// WMS.Application/Administrator/Labels/Services/LabelService.cs
using WMS.Application.Administrator.Labels.DTOs;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Labels.Services;

public class LabelService : ILabelService
{
    private readonly ILabelRepository _repo;
    private readonly IUnitOfWork _uow;

    public LabelService(ILabelRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<PagedResult<LabelDto>> GetListAsync(LabelFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);

        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<LabelDto>.Create(items, new PaginationQuery
        {
            Page = paged.Page,
            PageSize = paged.PageSize
        }, paged.TotalCount);
    }

    public async Task<LabelDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.LabelNotFound);

        return MapToDto(entity);
    }

    public async Task<List<LabelDto>> GetByEntityTypeAsync(EntityType entityType, bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetByEntityTypeAsync(entityType, onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<LabelDto> CreateAsync(CreateLabelRequest request, CancellationToken ct = default)
    {
        var name = request.Name.Trim();

        if (await _repo.ExistsNameAsync(name, request.EntityType, null, ct))
            throw new BadRequestException(MessageKeys.LabelNameAlreadyExists);

        var entity = new Label
        {
            Id = Guid.NewGuid(),
            Name = name,
            Color = request.Color?.Trim(),
            IsActive = request.IsActive,
            EntityType = request.EntityType,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task<LabelDto> UpdateAsync(Guid id, UpdateLabelRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.LabelNotFound);

        var name = request.Name.Trim();

        if (await _repo.ExistsNameAsync(name, request.EntityType, id, ct))
            throw new BadRequestException(MessageKeys.LabelNameAlreadyExists);

        entity.Name = name;
        entity.Color = request.Color?.Trim();
        entity.IsActive = request.IsActive;
        entity.EntityType = request.EntityType;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.LabelNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ToggleActiveAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.LabelNotFound);

        entity.IsActive = !entity.IsActive;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    // ========== Helpers ==========

    private static string GetEntityTypeName(EntityType type) => type switch
    {
        EntityType.Contractor => "پیمانکار",
        EntityType.Contract => "قرارداد",
        _ => type.ToString()
    };

    private static LabelDto MapToDto(Label entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Color = entity.Color,
        IsActive = entity.IsActive,
        EntityType = entity.EntityType,
        EntityTypeName = GetEntityTypeName(entity.EntityType),
        CreatedAt = entity.CreatedAt
    };
}