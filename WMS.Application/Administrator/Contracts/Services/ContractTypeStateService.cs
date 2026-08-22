// WMS.Application/Administrator/ContractTypeStates/Services/ContractTypeStateService.cs
using WMS.Application.Administrator.ContractTypeStates.DTOs;
using WMS.Application.Administrator.ContractTypeStates.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.ContractTypeStates.Services;

public class ContractTypeStateService : IContractTypeStateService
{
    private readonly IContractTypeStateRepository _repo;
    private readonly IUnitOfWork _uow;

    public ContractTypeStateService(IContractTypeStateRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<PagedResult<ContractTypeStateDto>> GetListAsync(
        ContractTypeStateFilterRequest filter,
        CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);
        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<ContractTypeStateDto>.Create(items, new PaginationQuery
        {
            Page = paged.Page,
            PageSize = paged.PageSize
        }, paged.TotalCount);
    }

    public async Task<List<ContractTypeStateDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetAllAsync(onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<ContractTypeStateDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeStateNotFound);

        return MapToDto(entity);
    }

    public async Task<ContractTypeStateDto> CreateAsync(CreateContractTypeStateRequest request, CancellationToken ct = default)
    {
        var title = request.Title.Trim();

        if (await _repo.ExistsTitleAsync(title, null, ct))
            throw new BadRequestException(MessageKeys.ContractTypeStateTitleAlreadyExists);

        var entity = new ContractTypeState
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

    public async Task<ContractTypeStateDto> UpdateAsync(
        Guid id,
        UpdateContractTypeStateRequest request,
        CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeStateNotFound);

        var title = request.Title.Trim();

        if (await _repo.ExistsTitleAsync(title, id, ct))
            throw new BadRequestException(MessageKeys.ContractTypeStateTitleAlreadyExists);

        entity.Title = title;
        entity.IsActive = request.IsActive;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeStateNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ToggleActiveAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractTypeStateNotFound);

        entity.IsActive = !entity.IsActive;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private static ContractTypeStateDto MapToDto(ContractTypeState entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        IsActive = entity.IsActive,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}