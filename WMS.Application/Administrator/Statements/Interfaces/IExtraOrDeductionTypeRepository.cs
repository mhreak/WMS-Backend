using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Statements.Interfaces;

public interface IExtraOrDeductionTypeRepository
{
    Task<List<ExtraOrDeductionType>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ExtraOrDeductionType?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(ExtraOrDeductionType entity, CancellationToken ct = default);
    Task UpdateAsync(ExtraOrDeductionType entity, CancellationToken ct = default);
}