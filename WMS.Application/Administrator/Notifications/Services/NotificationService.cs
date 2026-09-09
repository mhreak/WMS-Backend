// WMS.Application/Administrator/Notifications/Services/NotificationService.cs
using WMS.Application.Administrator.Notifications.DTOs;
using WMS.Application.Administrator.Notifications.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Notifications;
using WMS.Domain.Entities.Statements;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;
    private readonly IUnitOfWork _uow;

    private static readonly string[] AllowedFieldsForContract =
        { "StartDate", "FinishDate" };

    private static readonly string[] AllowedFieldsForContractStep =
        { "StartDate", "FinishDate", "DeadlineDate" };

    private static readonly string[] AllowedFieldsForStatement =
        { "StatementDate" };

    public NotificationService(INotificationRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<NotificationDto> CreateAsync(CreateNotificationRequest request, CancellationToken ct = default)
    {
        ValidateRequest(request.EntityType, request.FieldIndicator, request.Title);

        var entity = new Notification
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            Minutes = request.Minutes,
            IsActive = request.IsActive,
            EntityType = request.EntityType,
            FieldIndicator = request.FieldIndicator.Trim(),
            PeriodicCheck = request.PeriodicCheck,
            Config = request.Config,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task<NotificationDto> UpdateAsync(Guid id, UpdateNotificationRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.NotificationNotFound);

        ValidateRequest(request.EntityType, request.FieldIndicator, request.Title);

        entity.Title = request.Title.Trim();
        entity.Minutes = request.Minutes;
        entity.IsActive = request.IsActive;
        entity.EntityType = request.EntityType;
        entity.FieldIndicator = request.FieldIndicator.Trim();
        entity.PeriodicCheck = request.PeriodicCheck;
        entity.Config = request.Config;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToDto(entity);
    }

    public async Task<NotificationDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.NotificationNotFound);

        return MapToDto(entity);
    }

    public async Task<List<NotificationDto>> GetListAsync(bool onlyActive = false, CancellationToken ct = default)
    {
        var list = await _repo.GetAllAsync(onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.NotificationNotFound);

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    // ===================== Evaluate =====================

    public async Task<List<NotificationResultDto>> EvaluateAsync(CancellationToken ct = default)
    {
        var notifications = await _repo.GetActiveAsync(ct);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var result = new List<NotificationResultDto>();

        foreach (var notif in notifications)
        {
            var items = notif.EntityType switch
            {
                EntityType.Contract => await EvaluateContractsAsync(notif, today, ct),
                EntityType.ContractStep => await EvaluateContractStepsAsync(notif, today, ct),
                EntityType.ContractorStatement => await EvaluateStatementsAsync(notif, today, ct),
                _ => new List<NotificationResultDto>()
            };

            result.AddRange(items);
        }

        return result
            .OrderBy(x => x.DaysType == "remaining" ? 0 : 1)
            .ThenBy(x => Math.Abs(x.Days))
            .ToList();
    }

    private async Task<List<NotificationResultDto>> EvaluateContractsAsync(
        Notification notif, DateOnly today, CancellationToken ct)
    {
        var contracts = await _repo.GetContractsAsync(ct);
        var list = new List<NotificationResultDto>();

        foreach (var c in contracts)
        {
            DateOnly? date = notif.FieldIndicator switch
            {
                "StartDate" => c.StartDate,
                "FinishDate" => c.FinishedDate,
                _ => null
            };

            var item = BuildResult(notif, date, c.Id, c.Title, today);
            if (item != null)
                list.Add(item);
        }

        return list;
    }

    private async Task<List<NotificationResultDto>> EvaluateContractStepsAsync(
        Notification notif, DateOnly today, CancellationToken ct)
    {
        var steps = await _repo.GetOpenContractStepsAsync(ct);
        var list = new List<NotificationResultDto>();

        foreach (var s in steps)
        {
            DateOnly? date = notif.FieldIndicator switch
            {
                "StartDate" => s.StartDate,
                "FinishDate" => s.FinishDate,
                "DeadlineDate" => s.DeadlineDate,
                _ => null
            };

            var title = $"{s.Contract?.Title} / {s.Step?.Title}";
            var item = BuildResult(notif, date, s.ContractId, title, today);
            if (item != null)
                list.Add(item);
        }

        return list;
    }

    private async Task<List<NotificationResultDto>> EvaluateStatementsAsync(
        Notification notif, DateOnly today, CancellationToken ct)
    {
        var statements = await _repo.GetStatementsAsync(ct);
        var list = new List<NotificationResultDto>();

        foreach (var st in statements)
        {
            DateOnly? date = notif.FieldIndicator switch
            {
                "StatementDate" => st.StatementDate,
                _ => null
            };

            var title = $"{st.Contract?.Title} / {st.Title}";
            var item = BuildResult(notif, date, st.Id, title, today);
            if (item != null)
                list.Add(item);
        }

        return list;
    }

    /// <summary>
    /// ساخت نتیجه بر اساس Minutes مثبت/منفی
    /// </summary>
    private static NotificationResultDto? BuildResult(
        Notification notif,
        DateOnly? entityDate,
        Guid entityId,
        string entityTitle,
        DateOnly today)
    {
        if (entityDate is null)
            return null;

        // دقیقه → روز (تقریب)
        var offsetDays = notif.Minutes / (24 * 60);
        var targetDate = entityDate.Value.AddDays(offsetDays);
        var diff = targetDate.DayNumber - today.DayNumber; // >0 هنوز نرسیده | <0 گذشته

        string daysType;
        int days;

        if (notif.Minutes >= 0)
        {
            // بعد از تاریخ مرجع → روز مانده تا target
            daysType = "remaining";
            days = diff;

            // فقط مواردی که موعد رسیده یا گذشته (diff <= 0)
            // اگر می‌خواهی همه را نشان بدهی این if را بردار
            if (diff > 0)
                return null;
        }
        else
        {
            // قبل از تاریخ مرجع → روز گذشته از target
            daysType = "past";
            days = -diff;

            // فقط وقتی از target گذشته باشیم
            if (diff > 0)
                return null;
        }

        return new NotificationResultDto
        {
            NotificationId = notif.Id,
            Title = notif.Title,
            Days = Math.Abs(days),
            DaysType = daysType,
            EntityDate = entityDate.Value,
            EntityTitle = entityTitle,
            EntityId = entityId,
            EntityType = notif.EntityType
        };
    }

    // ===================== Helpers =====================

    private static void ValidateRequest(EntityType entityType, string fieldIndicator, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new BadRequestException(MessageKeys.NotificationTitleRequired);

        if (string.IsNullOrWhiteSpace(fieldIndicator))
            throw new BadRequestException(MessageKeys.NotificationFieldIndicatorRequired);

        var allowed = entityType switch
        {
            EntityType.Contract => AllowedFieldsForContract,
            EntityType.ContractStep => AllowedFieldsForContractStep,
            EntityType.ContractorStatement => AllowedFieldsForStatement,
            _ => Array.Empty<string>()
        };

        if (allowed.Length == 0)
            throw new BadRequestException(MessageKeys.InvalidEntityTypeForNotification);

        if (!allowed.Contains(fieldIndicator.Trim(), StringComparer.OrdinalIgnoreCase))
            throw new BadRequestException(MessageKeys.InvalidFieldIndicatorForEntity);
    }

    private static NotificationDto MapToDto(Notification e) => new()
    {
        Id = e.Id,
        Title = e.Title,
        Minutes = e.Minutes,
        IsActive = e.IsActive,
        EntityType = e.EntityType,
        FieldIndicator = e.FieldIndicator,
        PeriodicCheck = e.PeriodicCheck,
        Config = e.Config,
        CreatedAt = e.CreatedAt
    };
}