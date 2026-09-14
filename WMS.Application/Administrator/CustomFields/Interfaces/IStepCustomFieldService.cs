using WMS.Application.Administrator.CustomFields.DTOs;

namespace WMS.Application.Administrator.CustomFields.Interfaces;

public interface IStepCustomFieldService
{
    Task<List<StepCustomFieldDto>> GetStepFieldsAsync(Guid contractId, Guid stepId, CancellationToken ct = default);
    Task SetStepFieldsAsync(Guid contractId, Guid stepId, SetStepCustomFieldsRequest request, CancellationToken ct = default);
    Task<List<StepCustomFieldDefinitionDto>> GetFieldsByStepIdAsync(Guid stepId, CancellationToken ct = default);
}