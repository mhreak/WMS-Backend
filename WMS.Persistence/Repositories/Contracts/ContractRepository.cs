// WMS.Persistence/Repositories/Contracts/ContractRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.ContractBoard.DTOs;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Localization;
using WMS.Application.Common.Pagination;
using WMS.Domain.Entities.Contractors;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Enums;
using WMS.Persistence.Context;
using System.Text.Json;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Domain.Enums;

namespace WMS.Persistence.Repositories.Contracts;

public class ContractRepository : IContractRepository
{
    private readonly AppDbContext _db;

    public ContractRepository(AppDbContext db) => _db = db;

    public async Task<Contract?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Contract
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<Contract?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Contract
            .Include(x => x.Contractor)
                .ThenInclude(c => c!.Categories)  
            .Include(x => x.Categories)
            .Include(x => x.ContractType)
            .Include(x => x.ContractSteps)
            .ThenInclude(cs => cs.Step)
            .Include(x => x.ContractTypeState)
            .Include(x => x.File)
            .Include(x => x.Attachment)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }

    public async Task<PagedResult<Contract>> GetPagedAsync(ContractFilterRequest filter, CancellationToken ct = default)
    {
        var query = _db.Contract
            .AsNoTracking()
            .Include(x => x.Contractor)
            .Include(x => x.Categories)
            .Include(x => x.ContractType)
            .Include(x => x.ContractSteps)
                .ThenInclude(cs => cs.Step)
            .Include(x => x.ContractTypeState)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = $"%{filter.Search.Trim().ToLower()}%";
            query = query.Where(x =>
                EF.Functions.Like((x.Title ?? string.Empty).ToLower(), search) ||
                EF.Functions.Like((x.ContractNumber ?? string.Empty).ToLower(), search));
        }

        if (filter.ContractorId.HasValue)
            query = query.Where(x => x.ContractorId == filter.ContractorId.Value);


        if (filter.StartDateFrom.HasValue)
                query = query.Where(x => x.StartDate >= DateOnly.FromDateTime(filter.StartDateFrom.Value));

        if (filter.StartDateTo.HasValue)
                query = query.Where(x => x.StartDate <= DateOnly.FromDateTime(filter.StartDateTo.Value));
        query = query.OrderByDescending(x => x.CreatedAt);

        var pagination = new PaginationQuery
        {
            Page = filter.Page,
            PageSize = filter.PageSize
        }.Normalize();

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync(ct);

        return PagedResult<Contract>.Create(items, pagination, totalCount);
    }

    public async Task AddAsync(Contract entity, CancellationToken ct = default)
    {
        await _db.Contract.AddAsync(entity, ct);
    }

    public Task UpdateAsync(Contract entity, CancellationToken ct = default)
{
    entity.UpdatedAt = DateTime.UtcNow;
    return Task.CompletedTask;
}

    public Task SoftDeleteAsync(Contract entity, CancellationToken ct = default)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.Contract.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsContractNumberAsync(string contractNumber, Guid? excludeId = null, CancellationToken ct = default)
    {
        var query = _db.Contract
            .Where(x => !x.IsDeleted && x.ContractNumber == contractNumber);

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }
    public async Task<ContractBoardResponse> GetBoardAsync(
    ContractBoardFilterRequest filter,
    CancellationToken ct = default)
{
    var contractType = await _db.ContractTypes
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == filter.ContractTypeId && !x.IsDeleted, ct);

   var steps = await _db.ContractTypeStep
    .AsNoTracking()
    .Where(x => x.ContractTypeId == filter.ContractTypeId && !x.IsDeleted && x.IsActive)
    .OrderBy(x => x.StepOrder)
    .ToListAsync(ct);

    var query = _db.Contract_ContractTypeStep
        .AsNoTracking()
        .Include(x => x.Contract)
            .ThenInclude(c => c.Contractor)
        .Include(x => x.Step)
        .Where(x =>
            x.Step.ContractTypeId == filter.ContractTypeId &&
            x.FinishDate == null &&
            x.Contract != null &&
            !x.Contract.IsDeleted);

    if (filter.ContractorId.HasValue)
        query = query.Where(x => x.Contract.ContractorId == filter.ContractorId.Value);

    var activeItems = await query.ToListAsync(ct);

    return new ContractBoardResponse
    {
        ContractTypeId = filter.ContractTypeId,
        ContractTypeTitle = contractType?.Title,
        Steps = steps.Select(step =>
        {
            var cards = activeItems
                .Where(x => x.StepId == step.Id)
                .Select(x => new ContractBoardCardDto
                {
                    Id = x.ContractId,
                    Title = x.Contract.Title,
                    ContractNumber = x.Contract.ContractNumber,
                    ContractorId = x.Contract.ContractorId,
                    ContractorName = GetContractorName(x.Contract.Contractor),
                    StartDate = x.StartDate,
                    FinishedDate = x.FinishDate,
                    ContractStartDate = x.Contract.StartDate
                })
                .ToList();

            return new ContractBoardStepDto
            {
               StepId = step.Id,
                Title = step.Title,
                StepOrder = step.StepOrder,
                ContractsCount = cards.Count,
                Contracts = cards
            };
        }).ToList()
    };
}

public async Task MoveStepAsync(
    Guid contractId,
    MoveContractStepRequest request,
    CancellationToken ct = default)
{
    var contractExists = await _db.Contract
        .AnyAsync(x => x.Id == contractId && !x.IsDeleted, ct);

    if (!contractExists)
        throw new NotFoundException(MessageKeys.ContractNotFound);

    var targetStep = await _db.ContractTypeStep
        .FirstOrDefaultAsync(x => x.Id == request.TargetStepId && !x.IsDeleted, ct)
        ?? throw new NotFoundException(MessageKeys.StepNotFound);

    var today = DateOnly.FromDateTime(DateTime.UtcNow);
    var startDate = request.StartDate ?? today;
    var finishedDate = request.FinishedDate ?? today;

    var current = await _db.Contract_ContractTypeStep
        .Include(x => x.Step)
        .FirstOrDefaultAsync(x =>
            x.ContractId == contractId &&
            x.FinishDate == null, ct);

    // اولین ورود — هیچ مرحله بازی ندارد
    if (current is null)
    {
        var freshRow = await _db.Contract_ContractTypeStep
            .FirstOrDefaultAsync(x => x.ContractId == contractId && x.StepId == targetStep.Id, ct);

        if (freshRow is null)
        {
            await _db.Contract_ContractTypeStep.AddAsync(new ContractStep
            {
                ContractId = contractId,
                StepId = targetStep.Id,
                StartDate = startDate,
                FinishDate = null
            }, ct);
        }
        else
        {
            freshRow.FinishDate = null;
        }

        await _db.SaveChangesAsync(ct);
        return;
    }

    if (current.StepId == targetStep.Id)
        return;

    var allSteps = await _db.ContractTypeStep
        .Where(x => x.ContractTypeId == targetStep.ContractTypeId && !x.IsDeleted)
        .OrderBy(x => x.StepOrder)
        .ToListAsync(ct);

    var currentStepDef = current.Step ?? allSteps.FirstOrDefault(s => s.Id == current.StepId);
    if (currentStepDef is null)
        throw new NotFoundException(MessageKeys.StepNotFound);

    var isForward = targetStep.StepOrder > currentStepDef.StepOrder;

    if (isForward)
    {
        var errors = new List<IncompleteStepCustomFieldsError>();

        // فیلدهای اجباری مرحله فعلی
        var currentIncomplete = await GetIncompleteRequiredCustomFieldsAsync(
            contractId, currentStepDef.Id, ct);

        if (currentIncomplete.Count > 0)
        {
            errors.Add(new IncompleteStepCustomFieldsError
            {
                StepId = currentStepDef.Id,
                StepTitle = currentStepDef.Title,
                Fields = currentIncomplete
            });
        }

        var skippedSteps = allSteps
            .Where(s => s.StepOrder > currentStepDef.StepOrder
                     && s.StepOrder < targetStep.StepOrder)
            .ToList();

        var blockedStep = skippedSteps.FirstOrDefault(s => s.IsMandatory);
        if (blockedStep != null)
{
            throw new BadRequestException(
                MessageKeys.MandatoryStepCannotBeSkipped,
                "MANDATORY_STEP_CANNOT_BE_SKIPPED",
                new
                {
                    stepId = blockedStep.Id,
                    stepTitle = blockedStep.Title,
                    stepOrder = blockedStep.StepOrder
                });
        }

        // فیلدهای اجباری مراحل میانی
        foreach (var step in skippedSteps)
        {
            var incomplete = await GetIncompleteRequiredCustomFieldsAsync(contractId, step.Id, ct);
            if (incomplete.Count > 0)
            {
                errors.Add(new IncompleteStepCustomFieldsError
                {
                    StepId = step.Id,
                    StepTitle = step.Title,
                    Fields = incomplete
                });
            }
        }

        if (errors.Count > 0)
        {
            throw new BadRequestException(
                MessageKeys.RequiredCustomFieldsMissingOnStep,
                "REQUIRED_CUSTOM_FIELDS_MISSING",
                errors);
        }

        // بستن مرحله فعلی
        current.FinishDate = finishedDate;
        var cursorDate = finishedDate;

        // رد شدن از مراحل میانی
        foreach (var step in skippedSteps)
        {
            var row = await _db.Contract_ContractTypeStep
                .FirstOrDefaultAsync(x => x.ContractId == contractId && x.StepId == step.Id, ct);

            if (row is null)
            {
                await _db.Contract_ContractTypeStep.AddAsync(new ContractStep
                {
                    ContractId = contractId,
                    StepId = step.Id,
                    StartDate = cursorDate,
                    FinishDate = cursorDate
                }, ct);
            }
            else
            {
                row.FinishDate = cursorDate;
            }
        }

        // باز کردن مرحله هدف
        var targetRow = await _db.Contract_ContractTypeStep
            .FirstOrDefaultAsync(x => x.ContractId == contractId && x.StepId == targetStep.Id, ct);

        if (targetRow is null)
        {
            await _db.Contract_ContractTypeStep.AddAsync(new ContractStep
            {
                ContractId = contractId,
                StepId = targetStep.Id,
                StartDate = startDate,
                FinishDate = null
            }, ct);
        }
        else
        {
            targetRow.FinishDate = null;
        }
    }
    else
    {
        // حرکت به عقب
        current.FinishDate = finishedDate;

        var targetRow = await _db.Contract_ContractTypeStep
            .FirstOrDefaultAsync(x => x.ContractId == contractId && x.StepId == targetStep.Id, ct);

        if (targetRow != null)
        {
            targetRow.FinishDate = null;
        }
        else
        {
            await _db.Contract_ContractTypeStep.AddAsync(new ContractStep
            {
                ContractId = contractId,
                StepId = targetStep.Id,
                StartDate = startDate,
                FinishDate = null
            }, ct);
        }
    }

    await _db.SaveChangesAsync(ct);
}
// ===== Helper — چک می‌کنه آیا مرحله‌ای فیلد اجباری ناقص داره یا نه =====
private async Task<bool> HasIncompleteRequiredCustomFieldsAsync(Guid contractId, Guid stepId, CancellationToken ct)
{
    var candidateFields = await _db.EntityCustomField
        .Where(f => f.EntityType == EntityType.ContractStep && f.IsRequired && f.IsActive && !f.IsDeleted)
        .ToListAsync(ct);

    var requiredFieldIds = candidateFields
        .Where(f => ParseStepIdFromConfig(f.Config) == stepId)
        .Select(f => f.Id)
        .ToList();

    if (requiredFieldIds.Count == 0)
        return false;

    var filledCount = await _db.EntityCustomField_Entity 
        .CountAsync(v =>
            v.EntityId == contractId &&
            requiredFieldIds.Contains(v.EntityCustomFieldId) &&
            v.Value != null && v.Value != "", ct);

    return filledCount < requiredFieldIds.Count;
}

private static Guid? ParseStepIdFromConfig(string? configJson)
{
    if (string.IsNullOrWhiteSpace(configJson))
        return null;

    try
    {
        using var doc = System.Text.Json.JsonDocument.Parse(configJson);
        if (doc.RootElement.TryGetProperty("contractTypeStepId", out var prop) &&
            Guid.TryParse(prop.GetString(), out var id))
        {
            return id;
        }
    }
    catch
    {
        // JSON نامعتبر → نادیده گرفته می‌شه
    }

    return null;
}
private static string? GetContractorName(Contractor? contractor)
{
    if (contractor is null) return null;

    if (!string.IsNullOrWhiteSpace(contractor.CompanyName))
        return contractor.CompanyName;

    return string.Join(" ", new[] { contractor.FirstName, contractor.LastName }
        .Where(x => !string.IsNullOrWhiteSpace(x)));
}
private async Task<List<IncompleteCustomFieldItem>> GetIncompleteRequiredCustomFieldsAsync(
    Guid contractId,
    Guid stepId,
    CancellationToken ct)
{
    var allFields = await _db.EntityCustomField
        .AsNoTracking()
        .Where(x => x.EntityType == EntityType.ContractStep
                    && x.IsActive
                    && !x.IsDeleted
                    && x.IsRequired)
        .ToListAsync(ct);

    var stepFields = allFields
        .Where(f => ParseConfig(f.Config)?.ContractTypeStepId == stepId)
        .ToList();

    if (stepFields.Count == 0)
        return new List<IncompleteCustomFieldItem>();

    var fieldIds = stepFields.Select(f => f.Id).ToList();

    var values = await _db.EntityCustomField_Entity
        .AsNoTracking()
        .Where(v => v.EntityId == contractId && fieldIds.Contains(v.EntityCustomFieldId))
        .ToListAsync(ct);

    var incomplete = new List<IncompleteCustomFieldItem>();

    foreach (var field in stepFields)
    {
        var val = values.FirstOrDefault(v => v.EntityCustomFieldId == field.Id)?.Value;
        if (string.IsNullOrWhiteSpace(val))
        {
            incomplete.Add(new IncompleteCustomFieldItem
            {
                EntityCustomFieldId = field.Id,
                FieldName = field.FieldName
            });
        }
    }

    return incomplete;
}

private static EntityCustomFieldConfigShape? ParseConfig(string? json)
{
    if (string.IsNullOrWhiteSpace(json)) return null;

    try
    {
        return JsonSerializer.Deserialize<EntityCustomFieldConfigShape>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
    catch
    {
        return null;
    }
}

private class EntityCustomFieldConfigShape
{
    public Guid? ContractTypeStepId { get; set; }
}
}