// WMS.Application/Administrator/Notifications/Interfaces/INotificationService.cs
using WMS.Application.Administrator.Notifications.DTOs;

namespace WMS.Application.Administrator.Notifications.Interfaces;

public interface INotificationService
{
    Task<NotificationDto> CreateAsync(CreateNotificationRequest request, CancellationToken ct = default);
    Task<NotificationDto> UpdateAsync(Guid id, UpdateNotificationRequest request, CancellationToken ct = default);
    Task<NotificationDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<NotificationDto>> GetListAsync(bool onlyActive = false, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// ارزیابی نوتیفیکیشن‌های فعال روی entityهای تاریخ‌دار
    /// </summary>
    Task<List<NotificationResultDto>> EvaluateAsync(CancellationToken ct = default);
}