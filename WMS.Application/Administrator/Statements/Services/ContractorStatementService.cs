using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Statements.Services;

public class ContractorStatementService : IContractorStatementService
{
    private readonly IContractorStatementRepository _repo;
    private readonly IUnitOfWork _uow;

    public ContractorStatementService(IContractorStatementRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<PagedResult<ContractorStatementDto>> GetListAsync(ContractorStatementFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);
        var items = paged.Items.Select(MapToDto).ToList();

        return PagedResult<ContractorStatementDto>.Create(items,
            new PaginationQuery { Page = paged.Page, PageSize = paged.PageSize }, paged.TotalCount);
    }

    public async Task<ContractorStatementDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);
        return MapToDto(entity);
    }

    public async Task<ContractorStatementDto> CreateAsync(CreateContractorStatementRequest request, CancellationToken ct = default)
    {
        var entity = new ContractorStatement
        {
            Id = Guid.NewGuid(),
            ContractId = request.ContractId,
            ContractTypeStepId = request.ContractTypeStepId,
            StatementDate = request.StatementDate,
            FileName = request.FileName.Trim(),
            Amount = request.Amount,
            Description = request.Description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);
        return MapToDto(created);
    }

    public async Task<ContractorStatementDto> UpdateAsync(Guid id, UpdateContractorStatementRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        entity.ContractTypeStepId = request.ContractTypeStepId;
        entity.StatementDate = request.StatementDate;
        entity.FileName = request.FileName.Trim();
        entity.Amount = request.Amount;
        entity.Description = request.Description?.Trim();

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);
        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task<ContractorStatementDto> SetExtraOrDeductionsAsync(Guid statementId, SetStatementExtraOrDeductionsRequest request, CancellationToken ct = default)
    {
        var statement = await _repo.GetByIdAsync(statementId, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        var ruleIds = request.Items.Select(x => x.RuleId).Distinct().ToList();
        var rules = await _repo.GetRulesByIdsAsync(ruleIds, ct);

        if (rules.Count != ruleIds.Count)
            throw new NotFoundException(MessageKeys.ExtraOrDeductionRuleNotFoundInSet);

        var newItems = request.Items.Select(i => new ContractorStatementExtraOrDeduction
        {
            Id = Guid.NewGuid(),
            StatementId = statementId,
            RuleId = i.RuleId,
            Amount = i.Amount,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _repo.ReplaceItemsAsync(statementId, newItems, ct);
        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetByIdWithDetailsAsync(statementId, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);
        return MapToDto(updated);
    }

    private static ContractorStatementDto MapToDto(ContractorStatement e)
    {
        var items = e.ExtraOrDeductions
            .Where(i => !i.IsDeleted)
            .Select(i => new StatementExtraOrDeductionItemDto
            {
                Id = i.Id,
                RuleId = i.RuleId,
                RuleTitle = i.Rule?.ExtraOrDeductionType?.Title,
                IsExtra = i.Rule?.ExtraOrDeductionType?.IsExtra ?? false,
                Amount = i.Amount
            }).ToList();

        var netAmount = e.Amount
            + items.Where(i => i.IsExtra).Sum(i => i.Amount)
            - items.Where(i => !i.IsExtra).Sum(i => i.Amount);

        return new ContractorStatementDto
        {
            Id = e.Id,
            ContractId = e.ContractId,
            ContractTitle = e.Contract?.Title,
            ContractTypeStepId = e.ContractTypeStepId,
            ContractTypeStepTitle = e.ContractTypeStep?.Title,
            StatementDate = e.StatementDate,
            FileName = e.FileName,
            Amount = e.Amount,
            Description = e.Description,
            ExtraOrDeductions = items,
            NetAmount = netAmount,
            CreatedAt = e.CreatedAt
        };
    }
}