using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Statements;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Statements.Services;

public class ContractorStatementService : IContractorStatementService
{
    private readonly IContractorStatementRepository _repo;
    private readonly IEntityAttachmentService _attachmentService;
    private readonly IContractRepository _contractRepo;
    private readonly IExtraOrDeductionRuleRepository _ruleRepo;
    private readonly ILabelRepository _labelRepo;
    private readonly IUnitOfWork _uow;

    public ContractorStatementService(
        IContractorStatementRepository repo,
        IEntityAttachmentService attachmentService,
        IContractRepository contractRepo,
        IExtraOrDeductionRuleRepository ruleRepo,
        ILabelRepository labelRepo,
        IUnitOfWork uow)
    {
        _repo = repo;
        _attachmentService = attachmentService;
        _contractRepo = contractRepo;
        _ruleRepo = ruleRepo;
        _labelRepo = labelRepo;
        _uow = uow;
    }

    public async Task<PagedResult<ContractorStatementDto>> GetListAsync(ContractorStatementFilterRequest filter, CancellationToken ct = default)
    {
        var paged = await _repo.GetPagedAsync(filter, ct);
        var items = new List<ContractorStatementDto>();

        foreach (var statement in paged.Items)
        {
            var attachments = await _attachmentService.GetByEntityIdAsync(statement.Id, null, ct);
            items.Add(MapToDto(statement, attachments));
        }

        return PagedResult<ContractorStatementDto>.Create(items,
            new PaginationQuery { Page = paged.Page, PageSize = paged.PageSize }, paged.TotalCount);
    }

    public async Task<ContractorStatementDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        var attachments = await _attachmentService.GetByEntityIdAsync(id, null, ct);
        return MapToDto(entity, attachments);
    }

    public async Task<ContractorStatementDto> CreateAsync(CreateContractorStatementRequest request, CancellationToken ct = default)
    {
        var entity = new ContractorStatement
        {
            Id = Guid.NewGuid(),
            ContractId = request.ContractId,
            ContractTypeStepId = request.ContractTypeStepId,
            StatementDate = request.StatementDate,
            Amount = request.Amount,
            Description = request.Description?.Trim(),
            Title = request.Title.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        // ===== اعمال خودکار Ruleهای منطبق =====
        await ApplyMatchingRulesAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        // =======================================

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        var attachments = await _attachmentService.GetByEntityIdAsync(entity.Id, null, ct);
        return MapToDto(created, attachments);
    }

    public async Task<ContractorStatementDto> UpdateAsync(Guid id, UpdateContractorStatementRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        entity.ContractTypeStepId = request.ContractTypeStepId;
        entity.StatementDate = request.StatementDate;
        entity.Amount = request.Amount;
        entity.Description = request.Description?.Trim();
        entity.Title = request.Title.Trim();

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        // ===== چون Amount ممکنه عوض شده باشه، Ruleهای درصدی باید دوباره حساب بشن =====
        await ApplyMatchingRulesAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        // ==========================================================================

        var updated = await _repo.GetByIdWithDetailsAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        var attachments = await _attachmentService.GetByEntityIdAsync(id, null, ct);
        return MapToDto(updated, attachments);
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

    // ===== منطق اصلی تطبیق خودکار =====
    private async Task ApplyMatchingRulesAsync(ContractorStatement statement, CancellationToken ct)
    {
        var contract = await _contractRepo.GetByIdWithDetailsAsync(statement.ContractId, ct);

        if (contract?.ContractTypeId is null)
        {
            await _repo.ReplaceItemsAsync(statement.Id, new List<ContractorStatementExtraOrDeduction>(), ct);
            return;
        }

        var candidateRules = await _ruleRepo.GetActiveRulesByContractTypeAsync(contract.ContractTypeId.Value, ct);

        var contractCategoryIds = contract.Categories.Select(c => c.Id).ToHashSet();
        var contractorCategoryIds = contract.Contractor?.Categories.Select(c => c.Id).ToHashSet() ?? new HashSet<Guid>();
        var contractLabelIds = (await _labelRepo.GetEntityLabelsAsync(contract.Id, ct))
            .Select(el => el.LabelId).ToHashSet();

        var matchedRules = candidateRules.Where(rule =>
            (!rule.ContractCategoryId.HasValue || contractCategoryIds.Contains(rule.ContractCategoryId.Value)) &&
            (!rule.ContractorCategoryId.HasValue || contractorCategoryIds.Contains(rule.ContractorCategoryId.Value)) &&
            (!rule.ContractLabelId.HasValue || contractLabelIds.Contains(rule.ContractLabelId.Value)) &&
            (!rule.ContractorType.HasValue || contract.Contractor?.Type == rule.ContractorType.Value)
        ).ToList();

        var items = matchedRules.Select(rule => new ContractorStatementExtraOrDeduction
        {
            Id = Guid.NewGuid(),
            StatementId = statement.Id,
            RuleId = rule.Id,
            Amount = rule.AmountType == AmountType.Percentage
                ? (statement.Amount * rule.Amount) / 100
                : rule.Amount,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _repo.ReplaceItemsAsync(statement.Id, items, ct);
    }
    // ====================================

    private static ContractorStatementDto MapToDto(
        ContractorStatement e,
        List<Administrator.Attachments.DTOs.EntityAttachmentDto> attachments)
    {
        var allItems = e.ExtraOrDeductions
            .Where(i => !i.IsDeleted)
            .Select(i => new StatementExtraOrDeductionItemDto
            {
                Id = i.Id,
                RuleId = i.RuleId,
                RuleTitle = i.Rule?.ExtraOrDeductionType?.Title,
                IsExtra = i.Rule?.ExtraOrDeductionType?.IsExtra ?? false,
                AmountType = i.Rule?.AmountType ?? AmountType.Fixed,
                PercentageValue = i.Rule?.AmountType == AmountType.Percentage ? i.Rule.Amount : null,
                Amount = i.Amount
            }).ToList();

        var extras = allItems.Where(i => i.IsExtra).ToList();
        var deductions = allItems.Where(i => !i.IsExtra).ToList();
        var totalExtra = extras.Sum(i => i.Amount);
        var totalDeduction = deductions.Sum(i => i.Amount);

        return new ContractorStatementDto
        {
            Id = e.Id,
            Title = e.Title,
            ContractId = e.ContractId,
            ContractTitle = e.Contract?.Title,
            ContractTypeStepId = e.ContractTypeStepId,
            ContractTypeStepTitle = e.ContractTypeStep?.Title,
            StatementDate = e.StatementDate,
            Attachments = attachments.Select(a => new StatementAttachmentDto
            {
                Id = a.Id,
                FileName = a.FileName,
                Extension = a.Extension,
                Url = a.Url,
                ThumbnailUrl = a.ThumbnailUrl,
                Size = a.Size
            }).ToList(),
            Amount = e.Amount,
            Extras = extras,
            Deductions = deductions,
            TotalExtra = totalExtra,
            TotalDeduction = totalDeduction,
            NetAmount = e.Amount + totalExtra - totalDeduction,
            Description = e.Description,
            CreatedAt = e.CreatedAt
        };
    }
}