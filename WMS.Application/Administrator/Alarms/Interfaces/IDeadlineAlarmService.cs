using WMS.Application.Administrator.Alarms.DTOs;

namespace WMS.Application.Administrator.Alarms.Interfaces;

public interface IDeadlineAlarmService
{
    Task<DeadlineAlarmDto> CreateAsync(CreateDeadlineAlarmRequest request, CancellationToken ct = default);
    Task<DeadlineAlarmDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<DueAlarmDto>> GetDueAlarmsAsync(CancellationToken ct = default);
}