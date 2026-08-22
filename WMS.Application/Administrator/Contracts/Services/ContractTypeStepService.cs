// WMS.Application/Administrator/Steps/Services/StepService.cs
using WMS.Application.Administrator.ContractTypeSteps.DTOs;
using WMS.Application.Administrator.ContractTypeSteps.Interfaces;
using WMS.Application.Administrator.Steps.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.ContractTypeSteps.Services;

public class StepService : IStepService
{
    private readonly IStepRepository _repo;
    private readonly IUnitOfWork _uow;

    public StepService(IStepRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<PagedResult<StepDto>> GetListAsync(StepFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);
        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<StepDto>.Create(items, new PaginationQuery
        {
            Page = paged.Page,
            PageSize = paged.PageSize
        }, paged.TotalCount);
    }

    public async Task<List<StepDto>> GetByContractTypeAsync(Guid contractTypeId, bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetByContractTypeAsync(contractTypeId, onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<StepDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.StepNotFound);

        return MapToDto(entity);
    }

    public async Task<StepDto> CreateAsync(CreateStepRequest request, CancellationToken ct = default)
    {
        if (await _repo.ExistsOrderAsync(request.ContractTypeId, request.StepOrder, null, ct))
            throw new BadRequestException(MessageKeys.StepOrderAlreadyExists);

        var entity = new ContractTypeStep
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            IsActive = request.IsActive,
            StepOrder = request.StepOrder,
            ContractTypeId = request.ContractTypeId,
            IsMandatory = request.IsMandatory,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.StepNotFound);

        return MapToDto(created);
    }

    public async Task<StepDto> UpdateAsync(Guid id, UpdateStepRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.StepNotFound);

        if (await _repo.ExistsOrderAsync(request.ContractTypeId, request.StepOrder, id, ct))
            throw new BadRequestException(MessageKeys.StepOrderAlreadyExists);

        entity.Title = request.Title.Trim();
        entity.IsActive = request.IsActive;
        entity.ContractTypeId = request.ContractTypeId;
        entity.IsMandatory = request.IsMandatory;
        entity.StepOrder = request.StepOrder;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.StepNotFound);

        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.StepNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ToggleActiveAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.StepNotFound);

        entity.IsActive = !entity.IsActive;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private static StepDto MapToDto(ContractTypeStep entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        IsActive = entity.IsActive,
        IsMandatory = entity.IsMandatory,
        StepOrder = entity.StepOrder,
        ContractTypeId = entity.ContractTypeId,
        ContractTypeTitle = entity.ContractType?.Title,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}