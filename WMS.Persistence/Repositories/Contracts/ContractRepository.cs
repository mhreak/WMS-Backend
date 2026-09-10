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
using WMS.Persistence.Context;

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
            .Include(x => x.AttachmentFile)
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

    // مرحله‌ی باز فعلی
    var current = await _db.Contract_ContractTypeStep
        .Include(x => x.Step)
        .FirstOrDefaultAsync(x =>
            x.ContractId == contractId &&
            x.FinishDate == null, ct);

    // حالت اول ورود قرارداد (هیچ مرحله‌ی بازی نداره)
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

    // همه‌ی مراحل این نوع قرارداد، مرتب‌شده — برای پیداکردن مراحل بین مبدا و مقصد
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
        // مراحل میانی که داره ازشون رد می‌شه (بین فعلی و هدف، نه‌شامل خودشون)
        var skippedSteps = allSteps
            .Where(s => s.StepOrder > currentStepDef.StepOrder && s.StepOrder < targetStep.StepOrder)
            .ToList();

        var blockedStep = skippedSteps.FirstOrDefault(s => s.IsMandatory);
        if (blockedStep != null)
            throw new BadRequestException(MessageKeys.MandatoryStepCannotBeSkipped);

        // بستن مرحله‌ی فعلی
        current.FinishDate = finishedDate;
        var cursorDate = finishedDate;

        // رد شدن از مراحل میانی — هرکدوم همون روز باز و بسته می‌شن
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
                row.FinishDate = cursorDate; // اگه از قبل باز بوده (بازدید مجدد)، همین امروز می‌بندیمش
            }
        }

        // باز کردن مرحله‌ی هدف
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
            targetRow.FinishDate = null; // StartDate اصلی‌ش دست‌نخورده می‌مونه
        }
    }
    else
    {
        // بستن مرحله‌ی فعلی (که داره ترکش می‌کنه)
        current.FinishDate = finishedDate;

        // بازکردن مراحل بین هدف و فعلی (شامل خود هدف)
        var reopenSteps = allSteps
            .Where(s => s.StepOrder >= targetStep.StepOrder && s.StepOrder < currentStepDef.StepOrder)
            .ToList();

        foreach (var step in reopenSteps)
        {
            var row = await _db.Contract_ContractTypeStep
                .FirstOrDefaultAsync(x => x.ContractId == contractId && x.StepId == step.Id, ct);

            if (row != null)
            {
                row.FinishDate = null;   // StartDate تاریخی دست‌نخورده می‌مونه
            }
            else
            {
                // اگه قبلاً واقعاً ازش رد نشده بود (حالت غیرمنتظره)، تازه بازش می‌کنیم
                await _db.Contract_ContractTypeStep.AddAsync(new ContractStep
                {
                    ContractId = contractId,
                    StepId = step.Id,
                    StartDate = today,
                    FinishDate = null
                }, ct);
            }
        }
    }

    await _db.SaveChangesAsync(ct);
}
private static string? GetContractorName(Contractor? contractor)
{
    if (contractor is null) return null;

    if (!string.IsNullOrWhiteSpace(contractor.CompanyName))
        return contractor.CompanyName;

    return string.Join(" ", new[] { contractor.FirstName, contractor.LastName }
        .Where(x => !string.IsNullOrWhiteSpace(x)));
}
}