// WMS.Application/Administrator/Contractors/Services/ContractorService.cs
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Administrator.Contractors.Interfaces;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contractors;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Contractors.Services;

public class ContractorService : IContractorService
{
    private readonly IContractorRepository _repo;
    private readonly ILabelRepository _labelRepo;
    private readonly IUnitOfWork _uow;

    public ContractorService(
        IContractorRepository repo,
        ILabelRepository labelRepo,
        IUnitOfWork uow)
    {
        _repo = repo;
        _labelRepo = labelRepo;
        _uow = uow;
    }

    public async Task<PagedResult<ContractorListItemDto>> GetListAsync(ContractorFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);

        var items = new List<ContractorListItemDto>();
        foreach (var contractor in paged.Items)
        {
            var labels = await GetLabelsForEntityAsync(contractor.Id, ct);
            items.Add(MapToListItem(contractor, labels));
        }

        return PagedResult<ContractorListItemDto>.Create(items, new PaginationQuery
        {
            Page = paged.Page,
            PageSize = paged.PageSize
        }, paged.TotalCount);
    }

    public async Task<ContractorDetailDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorNotFound);

        var labels = await GetLabelsForEntityAsync(id, ct);
        return MapToDetail(entity, labels);
    }

    public async Task<ContractorDetailDto> CreateAsync(CreateContractorRequest request, CancellationToken ct = default)
    {
        await ValidateUniqueCodesAsync(request.NationalCode, request.EconomicCode, null, ct);

        var entity = new Contractor
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            FirstName = request.FirstName?.Trim(),
            LastName = request.LastName?.Trim(),
            CompanyName = request.CompanyName?.Trim(),
            CityId = request.CityId?? null,
            Mobile1 = request.Mobile1?.Trim(),
            Mobile2 = request.Mobile2?.Trim(),
            Phone1 = request.Phone1?.Trim(),
            Phone2 = request.Phone2?.Trim(),
            Email = request.Email?.Trim(),
            IsActive = request.IsActive,
            NationalCode = request.NationalCode?.Trim(),
            EconomicCode = request.EconomicCode?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        if (request.CategoryIds is { Count: > 0 })
        {
            var categories = await _repo.GetCategoriesByIdsAsync(request.CategoryIds, ct);
            foreach (var cat in categories)
                entity.Categories.Add(cat);
        }

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        await SyncLabelsAsync(entity.Id, request.LabelIds, ct);
        await _uow.SaveChangesAsync(ct);

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorNotFound);

        var labels = await GetLabelsForEntityAsync(entity.Id, ct);
        return MapToDetail(created, labels);
    }

    public async Task<ContractorDetailDto> UpdateAsync(Guid id, UpdateContractorRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorNotFound);

        await ValidateUniqueCodesAsync(request.NationalCode, request.EconomicCode, id, ct);

        entity.Type = request.Type;
        entity.FirstName = request.FirstName?.Trim();
        entity.LastName = request.LastName?.Trim();
        entity.CompanyName = request.CompanyName?.Trim();
        entity.CityId = request.CityId;
        entity.Mobile1 = request.Mobile1?.Trim();
        entity.Mobile2 = request.Mobile2?.Trim();
        entity.Phone1 = request.Phone1?.Trim();
        entity.Phone2 = request.Phone2?.Trim();
        entity.Email = request.Email?.Trim();
        entity.IsActive = request.IsActive;
        entity.NationalCode = request.NationalCode?.Trim();
        entity.EconomicCode = request.EconomicCode?.Trim();

        entity.Categories.Clear();
        if (request.CategoryIds is { Count: > 0 })
        {
            var categories = await _repo.GetCategoriesByIdsAsync(request.CategoryIds, ct);
            foreach (var cat in categories)
                entity.Categories.Add(cat);
        }

        await _repo.UpdateAsync(entity, ct);

        await SyncLabelsAsync(id, request.LabelIds, ct);

        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorNotFound);

        var labels = await GetLabelsForEntityAsync(id, ct);
        return MapToDetail(updated, labels);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _labelRepo.RemoveEntityLabelsAsync(id, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ToggleActiveAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorNotFound);

        entity.IsActive = !entity.IsActive;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    // ========== Categories ==========

    public async Task<List<ContractorCategoryDto>> GetCategoriesAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetAllCategoriesAsync(onlyActive, ct);
        return list.Select(c => MapCategoryToDto(c)).ToList();
    }

    public async Task<ContractorCategoryDto> CreateCategoryAsync(CreateContractorCategoryRequest request, CancellationToken ct = default)
    {
        var parent = await ValidateAndGetParentCategoryAsync(request.ParentId, null, ct);

        var entity = new ContractorCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            IsActive = request.IsActive,
            ParentId = request.ParentId,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddCategoryAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapCategoryToDto(entity, parent?.Name);
    }

    public async Task<ContractorCategoryDto> UpdateCategoryAsync(Guid id, UpdateContractorCategoryRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetCategoryByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorCategoryNotFound);

        var parent = await ValidateAndGetParentCategoryAsync(request.ParentId, id, ct);

        entity.Name = request.Name.Trim();
        entity.IsActive = request.IsActive;
        entity.ParentId = request.ParentId;

        await _repo.UpdateCategoryAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapCategoryToDto(entity, parent?.Name);
    }

    public async Task DeleteCategoryAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetCategoryByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorCategoryNotFound);

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateCategoryAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    // ========== Private Helpers ==========

    private async Task<ContractorCategory?> ValidateAndGetParentCategoryAsync(Guid? parentId, Guid? currentId, CancellationToken ct)
    {
        if (!parentId.HasValue) return null;

        if (currentId.HasValue && parentId.Value == currentId.Value)
            throw new BadRequestException(MessageKeys.CategoryCannotBeOwnParent);

        var parent = await _repo.GetCategoryByIdAsync(parentId.Value, ct)
            ?? throw new NotFoundException(MessageKeys.CategoryParentNotFound);

        if (currentId.HasValue)
        {
            var cursor = parent;
            while (cursor?.ParentId != null)
            {
                if (cursor.ParentId.Value == currentId.Value)
                    throw new BadRequestException(MessageKeys.CategoryCircularParent);

                cursor = await _repo.GetCategoryByIdAsync(cursor.ParentId.Value, ct);
            }
        }

        return parent;
    }

    private static ContractorCategoryDto MapCategoryToDto(ContractorCategory e, string? parentName = null) => new()
    {
        Id = e.Id,
        Name = e.Name,
        IsActive = e.IsActive,
        ParentId = e.ParentId,
        ParentName = parentName ?? e.Parent?.Name ?? string.Empty,
        CreatedAt = e.CreatedAt
    };

    private async Task ValidateUniqueCodesAsync(string? nationalCode, string? economicCode, Guid? excludeId, CancellationToken ct)
    {
        if (!string.IsNullOrWhiteSpace(nationalCode) &&
            await _repo.ExistsNationalCodeAsync(nationalCode, excludeId, ct))
        {
            throw new BadRequestException(MessageKeys.NationalCodeAlreadyExists);
        }

        if (!string.IsNullOrWhiteSpace(economicCode) &&
            await _repo.ExistsEconomicCodeAsync(economicCode, excludeId, ct))
        {
            throw new BadRequestException(MessageKeys.EconomicCodeAlreadyExists);
        }
    }

    private async Task SyncLabelsAsync(Guid entityId, List<Guid>? labelIds, CancellationToken ct)
    {
        await _labelRepo.RemoveEntityLabelsAsync(entityId, ct);

        if (labelIds is not { Count: > 0 }) return;

        foreach (var labelId in labelIds.Distinct())
        {
            var entityLabel = new EntityLabel
            {
                LabelId = labelId,
                EntityId = entityId,
            };
            await _labelRepo.AddEntityLabelAsync(entityLabel, ct);
        }
    }

    private async Task<List<LookupItemDto>> GetLabelsForEntityAsync(Guid entityId, CancellationToken ct)
    {
        var entityLabels = await _labelRepo.GetEntityLabelsAsync(entityId, ct);

        return entityLabels
            .Where(x => x.Label != null && x.Label.IsActive && !x.Label.IsDeleted)
            .Select(x => new LookupItemDto
            {
                Id = x.Label.Id,
                Name = x.Label.Name,
                Color = x.Label.Color
            })
            .ToList();
    }

    private static string GetTypeName(ContractorType type) => type switch
    {
        ContractorType.Individual => "حقیقی",
        ContractorType.Legal => "حقوقی",
        _ => type.ToString()
    };

    private static string? GetDisplayName(Contractor c)
    {
        if (c.Type == ContractorType.Legal)
            return c.CompanyName;

        var parts = new[] { c.FirstName, c.LastName }.Where(x => !string.IsNullOrWhiteSpace(x));
        return string.Join(" ", parts);
    }

    private static ContractorListItemDto MapToListItem(Contractor c, List<LookupItemDto> labels) => new()
    {
        Id = c.Id,
        Type = c.Type,
        TypeName = GetTypeName(c.Type),
        FirstName = c.FirstName,
        LastName = c.LastName,
        CompanyName = c.CompanyName,
        DisplayName = GetDisplayName(c),
        Mobile1 = c.Mobile1,
        Mobile2 = c.Mobile2,
        Phone1 = c.Phone1,
        Phone2 = c.Phone2,
        Email = c.Email,
        IsActive = c.IsActive,
        ProvinceId = c.City?.ProvinceId,
        ProvinceName = c.City?.Province?.Name,
        CityId = c.CityId,
        CityName = c.City?.Name,
        Categories = c.Categories
            .Where(cat => !cat.IsDeleted)
            .Select(cat => new LookupItemDto { Id = cat.Id, Name = cat.Name })
            .ToList(),
        Labels = labels,
        CreatedAt = c.CreatedAt
    };

    private static ContractorDetailDto MapToDetail(Contractor c, List<LookupItemDto> labels) => new()
    {
        Id = c.Id,
        Type = c.Type,
        TypeName = GetTypeName(c.Type),
        FirstName = c.FirstName,
        LastName = c.LastName,
        CompanyName = c.CompanyName,
        ProvinceId = c.City?.ProvinceId,
        ProvinceName = c.City?.Province?.Name,
        CityId = c.CityId,
        CityName = c.City?.Name,
        Mobile1 = c.Mobile1,
        Mobile2 = c.Mobile2,
        Phone1 = c.Phone1,
        Phone2 = c.Phone2,
        Email = c.Email,
        IsActive = c.IsActive,
        NationalCode = c.NationalCode,
        EconomicCode = c.EconomicCode,
        Categories = c.Categories
            .Where(cat => !cat.IsDeleted)
            .Select(cat => new LookupItemDto { Id = cat.Id, Name = cat.Name })
            .ToList(),
        Labels = labels,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt
    };
}