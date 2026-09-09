using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Notifications.DTOs;

public class CreateNotificationRequest
{
    public string Title { get; set; } = string.Empty;
    public int Minutes { get; set; }
    public bool IsActive { get; set; } = true;
    public EntityType EntityType { get; set; }
    public string FieldIndicator { get; set; } = string.Empty;
    public bool PeriodicCheck { get; set; }
    public string? Config { get; set; }
}

public class UpdateNotificationRequest
{
    public string Title { get; set; } = string.Empty;
    public int Minutes { get; set; }
    public bool IsActive { get; set; }
    public EntityType EntityType { get; set; }
    public string FieldIndicator { get; set; } = string.Empty;
    public bool PeriodicCheck { get; set; }
    public string? Config { get; set; }
}

public class NotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Minutes { get; set; }
    public bool IsActive { get; set; }
    public EntityType EntityType { get; set; }
    public string FieldIndicator { get; set; } = string.Empty;
    public bool PeriodicCheck { get; set; }
    public string? Config { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// خروجی API ارزیابی نوتیفیکیشن‌ها
/// </summary>
public class NotificationResultDto
{
    public Guid NotificationId { get; set; }
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// اگر Minutes مثبت بوده → روز مانده (باقی‌مانده تا target)
    /// اگر Minutes منفی بوده → روز گذشته (چند روز از target گذشته)
    /// </summary>
    public int Days { get; set; }

    /// <summary>
    /// "remaining" یا "past"
    /// </summary>
    public string DaysType { get; set; } = string.Empty;

    public DateOnly EntityDate { get; set; }
    public string EntityTitle { get; set; } = string.Empty;
    public Guid EntityId { get; set; }
    public EntityType EntityType { get; set; }
}