// WMS.Application/Administrator/Contracts/Services/ContractService.cs
using WMS.Application.Administrator.ContractBoard.DTOs;
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Administrator.ContractTypeSteps.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Contracts.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _repo;
    private readonly IUnitOfWork _uow;

    private readonly IContractCategoryRepository _categoryRepo;
    private readonly IStepRepository _stepRepo;

    public ContractService(IContractRepository repo, IUnitOfWork uow , IContractCategoryRepository categoryRepo, IStepRepository stepRepo)
    {
        _repo = repo;
        _uow = uow;
        _categoryRepo = categoryRepo;
        _stepRepo = stepRepo;

    }

    public async Task<PagedResult<ContractDto>> GetListAsync(ContractFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);
        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<ContractDto>.Create(items, new PaginationQuery
        {
            Page = paged.Page,
            PageSize = paged.PageSize
        }, paged.TotalCount);
    }

    public async Task<ContractDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractNotFound);

        return MapToDto(entity);
    }

    public async Task<ContractDto> CreateAsync(CreateContractRequest request, CancellationToken ct = default)
    {
        var contractNumber = request.ContractNumber.Trim();

        if (await _repo.ExistsContractNumberAsync(contractNumber, null, ct))
            throw new BadRequestException(MessageKeys.ContractNumberAlreadyExists);

        if (request.FinishedDate.HasValue && request.FinishedDate < request.StartDate)
            throw new BadRequestException(MessageKeys.InvalidContractDates);

        var entity = new Contract
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            ContractorId = request.ContractorId,
            ContractAmount = request.ContractAmount,
            ContractNumber = contractNumber,
            ContractTypeId = request.ContractTypeId,
            StartDate = DateOnly.FromDateTime(request.StartDate),
            FinishedDate = request.FinishedDate.HasValue
                ? DateOnly.FromDateTime(request.FinishedDate.Value)
                : null,
            CreatedAt = DateTime.UtcNow
        };

        if (request.CategoryIds is { Count: > 0 })
        {
            var categories = await _categoryRepo.GetByIdsAsync(request.CategoryIds, ct);
            foreach (var cat in categories)
                entity.Categories.Add(cat);
        }

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        // ===== جدید: ورود خودکار به اولین مرحله‌ی نوع قرارداد =====
        if (entity.ContractTypeId.HasValue)
{
    var steps = await _stepRepo.GetByContractTypeAsync(entity.ContractTypeId.Value, onlyActive: true, ct);
    var firstStep = steps.OrderBy(s => s.StepOrder).FirstOrDefault();

    if (firstStep != null)
    {
        await _repo.MoveStepAsync(entity.Id, new MoveContractStepRequest
        {
            TargetStepId = firstStep.Id,
            StartDate = entity.StartDate
        }, ct);
    }
}
        // ==========================================================

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractNotFound);

        return MapToDto(created);
    }

    public async Task<ContractDto> UpdateAsync(Guid id, UpdateContractRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractNotFound);

        var contractNumber = request.ContractNumber.Trim();

        if (await _repo.ExistsContractNumberAsync(contractNumber, id, ct))
            throw new BadRequestException(MessageKeys.ContractNumberAlreadyExists);

        if (request.FinishedDate.HasValue && request.FinishedDate < request.StartDate)
            throw new BadRequestException(MessageKeys.InvalidContractDates);

        entity.Title = request.Title.Trim();
        entity.ContractorId = request.ContractorId;
        entity.ContractAmount = request.ContractAmount;
        entity.ContractNumber = contractNumber;
        entity.ContractTypeId = request.ContractTypeId;
        entity.StartDate    = DateOnly.FromDateTime(request.StartDate);
        entity.FinishedDate = request.FinishedDate is null 
        ? null 
        : DateOnly.FromDateTime(request.FinishedDate.Value);
        
        if (request.CategoryIds is { Count: > 0 })
        {
            var categories = await _categoryRepo.GetByIdsAsync(request.CategoryIds, ct);
            foreach (var cat in categories)
                entity.Categories.Add(cat);
        }
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractNotFound);

        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractNotFound);

        await _repo.SoftDeleteAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }


    private static string? GetContractorDisplayName(Contract entity)
    {
        var c = entity.Contractor;
        if (c is null) return null;

        if (c.Type == ContractorType.Legal)
            return c.CompanyName;

        var parts = new[] { c.FirstName, c.LastName }.Where(x => !string.IsNullOrWhiteSpace(x));
        return string.Join(" ", parts);
    }

   private static ContractDto MapToDto(Contract entity)
{
    var currentStep = entity.ContractSteps?
        .FirstOrDefault(cs => cs.FinishDate == null);

    return new ContractDto
    {
        Id = entity.Id,
        Title = entity.Title,
        ContractorId = entity.ContractorId,
        ContractorName = GetContractorDisplayName(entity),
        ContractAmount = entity.ContractAmount ?? 0,
        ContractNumber = entity.ContractNumber,
        StartDate = entity.StartDate ?? default,
        FinishedDate = entity.FinishedDate,
        Categories = entity.Categories?
            .Select(c => new LookupItemDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList() ?? new List<LookupItemDto>(),

        CurrentStepId = currentStep?.StepId,
        CurrentStepTitle = currentStep?.Step?.Title,
        CurrentStepStartDate = currentStep?.StartDate,
        ContractTypeStateId = entity.ContractTypeStateId,
        ContractTypeStateTitle = entity.ContractTypeState?.Title ?? string.Empty,
        

        ContractTypeId = entity.ContractTypeId,
        ContractTypeTitle = entity.ContractType?.Title ?? string.Empty,

        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}
// داخل ContractService

public Task<ContractBoardResponse> GetBoardAsync(
    ContractBoardFilterRequest filter,
    CancellationToken ct = default)
{
    return _repo.GetBoardAsync(filter, ct);
}

public Task MoveStepAsync(
    Guid contractId,
    MoveContractStepRequest request,
    CancellationToken ct = default)
{
    return _repo.MoveStepAsync(contractId, request, ct);
}
    
}