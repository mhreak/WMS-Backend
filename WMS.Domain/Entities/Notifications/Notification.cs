using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.Notifications;

public class Notification : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// مثبت = بعد از تاریخ مرجع | منفی = قبل از تاریخ مرجع
    /// واحد: دقیقه
    /// </summary>
    [Required]
    public int Minutes { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public EntityType EntityType { get; set; }

    /// <summary>
    /// فیلد تاریخی که باید روی آن اعمال شود
    /// مثلاً: StartDate, FinishDate, StatementDate, DeadlineDate
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string FieldIndicator { get; set; } = string.Empty;

    /// <summary>
    /// اگر true باشد، به صورت دوره‌ای چک می‌شود
    /// </summary>
    public bool PeriodicCheck { get; set; }

    /// <summary>
    /// تنظیمات اضافه (JSON)
    /// </summary>
    public string? Config { get; set; }
}