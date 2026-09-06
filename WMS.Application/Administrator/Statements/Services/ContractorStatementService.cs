using WMS.Application.Administrator.Attachments.Interfaces;
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
    private readonly IEntityAttachmentService _attachmentService;   // جدید، به‌جای IEntityAttachmentRepository/IConfiguration
    private readonly IUnitOfWork _uow;

    public ContractorStatementService(
        IContractorStatementRepository repo,
        IEntityAttachmentService attachmentService,
        IUnitOfWork uow)
    {
        _repo = repo;
        _attachmentService = attachmentService;
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

        var created = await _repo.GetByIdWithDetailsAsync(entity.Id, ct)
            ?? throw new NotFoundException(MessageKeys.ContractorStatementNotFound);

        // تازه ساخته شده، هنوز فایلی وصل نشده
        return MapToDto(created, new List<Administrator.Attachments.DTOs.EntityAttachmentDto>());
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

        var attachments = await _attachmentService.GetByEntityIdAsync(statementId, null, ct);
        return MapToDto(updated, attachments);
    }

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
                AmountType = i.Rule?.AmountType ?? Domain.Enums.AmountType.Fixed,
                PercentageValue = i.Rule?.AmountType == Domain.Enums.AmountType.Percentage ? i.Rule.Amount : null,
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