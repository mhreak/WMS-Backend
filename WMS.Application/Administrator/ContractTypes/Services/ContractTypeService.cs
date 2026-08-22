// WMS.Application/Administrator/ContractTypes/Services/ContractTypeService.cs
using WMS.Application.Administrator.ContractTypes.DTOs;
using WMS.Application.Administrator.ContractTypes.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Application.Administrator.ContractTypes.Services;

public class ContractTypeService : IContractTypeService
{
    private readonly IContractTypeRepository _repo;
    private readonly IUnitOfWork _uow;

    public ContractTypeService(IContractTypeRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<PagedResult<ContractTypeDto>> GetListAsync(ContractTypeFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);

        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<ContractTypeDto>.Create(items, new PaginationQuery
        {
            Page = paged.Page,
            PageSize = paged.PageSize
        }, paged.TotalCount);
    }

    public async Task<List<ContractTypeDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetAllAsync(onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<ContractTypeDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeNotFound);

        return MapToDto(entity);
    }

    public async Task<ContractTypeDto> CreateAsync(CreateContractTypeRequest request, CancellationToken ct = default)
    {
        var title = request.Title.Trim();

        if (await _repo.ExistsTitleAsync(title, null, ct))
            throw new BadRequestException(MessageKeys.ContractTypeTitleAlreadyExists);

        var entity = new ContractType
        {
            Id = Guid.NewGuid(),
            Title = title,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task<ContractTypeDto> UpdateAsync(Guid id, UpdateContractTypeRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeNotFound);

        var title = request.Title.Trim();

        if (await _repo.ExistsTitleAsync(title, id, ct))
            throw new BadRequestException(MessageKeys.ContractTypeTitleAlreadyExists);

        entity.Title = title;
        entity.IsActive = request.IsActive;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ToggleActiveAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeNotFound);

        entity.IsActive = !entity.IsActive;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private static ContractTypeDto MapToDto(ContractType entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        IsActive = entity.IsActive,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}