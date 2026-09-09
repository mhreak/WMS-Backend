// WMS.Application/Administrator/Notifications/Interfaces/INotificationRepository.cs
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Notifications;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Notifications.Interfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Notification>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default);
    Task<List<Notification>> GetActiveAsync(CancellationToken ct = default);
    Task AddAsync(Notification entity, CancellationToken ct = default);
    Task UpdateAsync(Notification entity, CancellationToken ct = default);

    // برای Evaluate
    Task<List<Contract>> GetContractsAsync(CancellationToken ct = default);
    Task<List<ContractStep>> GetOpenContractStepsAsync(CancellationToken ct = default);
    Task<List<ContractorStatement>> GetStatementsAsync(CancellationToken ct = default);
}