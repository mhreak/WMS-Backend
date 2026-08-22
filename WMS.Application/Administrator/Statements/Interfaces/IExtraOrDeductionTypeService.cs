using WMS.Application.Administrator.Statements.DTOs;

namespace WMS.Application.Administrator.Statements.Interfaces;

public interface IExtraOrDeductionTypeService
{
    Task<List<ExtraOrDeductionTypeDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
    Task<ExtraOrDeductionTypeDto> CreateAsync(CreateExtraOrDeductionTypeRequest request, CancellationToken ct = default);
    Task<ExtraOrDeductionTypeDto> UpdateAsync(Guid id, UpdateExtraOrDeductionTypeRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}