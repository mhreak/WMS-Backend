using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Statements.Services;

public class ExtraOrDeductionRuleService : IExtraOrDeductionRuleService
{
    private readonly IExtraOrDeductionRuleRepository _repo;
    private readonly IExtraOrDeductionTypeRepository _typeRepo;
    private readonly IUnitOfWork _uow;

    public ExtraOrDeductionRuleService(
        IExtraOrDeductionRuleRepository repo,
        IExtraOrDeductionTypeRepository typeRepo,
        IUnitOfWork uow)
    {
        _repo = repo;
        _typeRepo = typeRepo;
        _uow = uow;
    }

    public async Task<PagedResult<ExtraOrDeductionRuleDto>> GetListAsync(ExtraOrDeductionRuleFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);
        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<ExtraOrDeductionRuleDto>.Create(items,
            new PaginationQuery { Page = paged.Page, PageSize = paged.PageSize }, paged.TotalCount);
    }

    public async Task<ExtraOrDeductionRuleDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionRuleNotFound);
        return MapToDto(entity);
    }

    public async Task<ExtraOrDeductionRuleDto> CreateAsync(CreateExtraOrDeductionRuleRequest request, CancellationToken ct = default)
    {
        await ValidateTypeExistsAsync(request.ExtraOrDeductionTypeId, ct);

        var entity = new ExtraOrDeductionRule
{
    Id = Guid.NewGuid(),
    ContractTypeId = request.ContractTypeId,
    ContractorType = request.ContractorType,
    ExtraOrDeductionTypeId = request.ExtraOrDeductionTypeId,
    AmountType = request.AmountType,
    Amount = request.Amount,
    ContractorCategoryId = request.ContractorCategoryId,
    ContractCategoryId = request.ContractCategoryId,
    ContractLabelId = request.ContractLabelId,
    CreatedAt = DateTime.UtcNow
};
        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionRuleNotFound);
        return MapToDto(created);
    }

    public async Task<ExtraOrDeductionRuleDto> UpdateAsync(Guid id, UpdateExtraOrDeductionRuleRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionRuleNotFound);

        await ValidateTypeExistsAsync(request.ExtraOrDeductionTypeId, ct);

        entity.ContractTypeId = request.ContractTypeId;
        entity.ContractorType = request.ContractorType;
        entity.ExtraOrDeductionTypeId = request.ExtraOrDeductionTypeId;
        entity.AmountType = request.AmountType;
        entity.Amount = request.Amount;
        entity.ContractorCategoryId = request.ContractorCategoryId;
        entity.ContractCategoryId = request.ContractCategoryId;
        entity.ContractLabelId = request.ContractLabelId;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionRuleNotFound);
        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionRuleNotFound);

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private async Task ValidateTypeExistsAsync(Guid typeId, CancellationToken ct)
    {
        _ = await _typeRepo.GetByIdAsync(typeId, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionTypeNotFound);
    }

    private static ExtraOrDeductionRuleDto MapToDto(ExtraOrDeductionRule e) => new()
{
    Id = e.Id,
    ContractTypeId = e.ContractTypeId,
    ContractTypeTitle = e.ContractType?.Title,
    ContractorType = e.ContractorType,
    ExtraOrDeductionTypeId = e.ExtraOrDeductionTypeId,
    ExtraOrDeductionTypeTitle = e.ExtraOrDeductionType?.Title,
    IsExtra = e.ExtraOrDeductionType?.IsExtra ?? false,
    AmountType = e.AmountType,
    Amount = e.Amount,

    ContractorCategoryId = e.ContractorCategoryId,
    ContractorCategoryName = e.ContractorCategory?.Name,

    ContractCategoryId = e.ContractCategoryId,
    ContractCategoryName = e.ContractCategory?.Name,


    ContractLabelId = e.ContractLabelId,
    ContractLabelName = e.ContractLabel?.Name,

    CreatedAt = e.CreatedAt
};
}