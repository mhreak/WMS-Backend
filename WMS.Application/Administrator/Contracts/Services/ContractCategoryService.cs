// ContractCategoryService.cs
using WMS.Application.Administrator.ContractCategories.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Contracts;

public class ContractCategoryService : IContractCategoryService
{
    private readonly IContractCategoryRepository _repo;
    private readonly IUnitOfWork _uow;

    public ContractCategoryService(IContractCategoryRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<List<ContractCategoryDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetAllAsync(onlyActive, ct);
        return list.Select(x => MapToDto(x)).ToList();
    }

    public async Task<ContractCategoryDto> CreateAsync(CreateContractCategoryRequest request, CancellationToken ct = default)
    {
        var parent = await ValidateAndGetParentAsync(request.ParentId, null, ct);

        var entity = new ContractCategory
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            IsActive = request.IsActive,
            ParentId = request.ParentId,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(entity, parent?.Name);
    }

    public async Task<ContractCategoryDto> UpdateAsync(Guid id, UpdateContractCategoryRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractCategoryNotFound);

        var parent = await ValidateAndGetParentAsync(request.ParentId, id, ct);

        entity.Name = request.Name.Trim();
        entity.IsActive = request.IsActive;
        entity.ParentId = request.ParentId;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(entity, parent?.Name);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractCategoryNotFound);

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    // اعتبارسنجی والد: نباید خودش باشه، باید وجود داشته باشه، و نباید باعث حلقه بشه
    private async Task<ContractCategory?> ValidateAndGetParentAsync(Guid? parentId, Guid? currentId, CancellationToken ct)
    {
        if (!parentId.HasValue) return null;

        if (currentId.HasValue && parentId.Value == currentId.Value)
            throw new BadRequestException(MessageKeys.CategoryCannotBeOwnParent);

        var parent = await _repo.GetByIdAsync(parentId.Value, ct)
            ?? throw new NotFoundException(MessageKeys.CategoryParentNotFound);

        if (currentId.HasValue)
        {
            var cursor = parent;
            while (cursor?.ParentId != null)
            {
                if (cursor.ParentId.Value == currentId.Value)
                    throw new BadRequestException(MessageKeys.CategoryCircularParent);

                cursor = await _repo.GetByIdAsync(cursor.ParentId.Value, ct);
            }
        }

        return parent;
    }

    private static ContractCategoryDto MapToDto(ContractCategory e, string? parentName = null) => new()
    {
        Id = e.Id,
        Name = e.Name,
        IsActive = e.IsActive,
        ParentId = e.ParentId,
        ParentName = parentName ?? e.Parent?.Name ?? string.Empty,
        CreatedAt = e.CreatedAt
    };
}