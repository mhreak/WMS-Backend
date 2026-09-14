using WMS.Application.Administrator.CustomFields.DTOs;
using WMS.Application.Administrator.CustomFields.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.CustomFields.Services;

public class StepCustomFieldService : IStepCustomFieldService
{
    private readonly IEntityCustomFieldRepository _repo;
    private readonly IUnitOfWork _uow;

    public StepCustomFieldService(IEntityCustomFieldRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<List<StepCustomFieldDto>> GetStepFieldsAsync(Guid contractId, Guid stepId, CancellationToken ct = default)
    {
        var allFields = await _repo.GetByEntityTypeAsync(EntityType.ContractStep, ct);
        var stepFields = FilterByStepId(allFields, stepId);

        if (stepFields.Count == 0)
            return new List<StepCustomFieldDto>();

        var values = await _repo.GetValuesAsync(contractId, stepFields.Select(f => f.Id).ToList(), ct);

        return stepFields.Select(f => new StepCustomFieldDto
        {
            EntityCustomFieldId = f.Id,
            FieldName = f.FieldName,
            FieldType = f.FieldType,
            IsRequired = f.IsRequired,
            Value = values.FirstOrDefault(v => v.EntityCustomFieldId == f.Id)?.Value
        }).ToList();
    }

    public async Task SetStepFieldsAsync(Guid contractId, Guid stepId, SetStepCustomFieldsRequest request, CancellationToken ct = default)
    {
        var allFields = await _repo.GetByEntityTypeAsync(EntityType.ContractStep, ct);
        var stepFields = FilterByStepId(allFields, stepId);
        var stepFieldIds = stepFields.Select(f => f.Id).ToHashSet();

        // فقط فیلدهایی که واقعاً مال همین مرحله‌ان قبول می‌شن (جلوگیری از دستکاری فیلدهای مراحل دیگه)
        var validItems = request.Values.Where(v => stepFieldIds.Contains(v.EntityCustomFieldId)).ToList();

        foreach (var item in validItems)
        {
            await _repo.UpsertValueAsync(item.EntityCustomFieldId, contractId, item.Value, ct);
        }

        await _uow.SaveChangesAsync(ct);
    }

    private static List<Domain.Entities.CustomFields.EntityCustomField> FilterByStepId(
        List<Domain.Entities.CustomFields.EntityCustomField> fields, Guid stepId)
    {
        return fields.Where(f =>
        {
            var config = ParseConfig(f.Config);
            return config?.ContractTypeStepId == stepId;
        }).ToList();
    }

    private static EntityCustomFieldConfigShape? ParseConfig(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<EntityCustomFieldConfigShape>(json);
        }
        catch { return null; }
    }

    private class EntityCustomFieldConfigShape
    {
        public Guid? ContractTypeStepId { get; set; }
    }

    public async Task<List<StepCustomFieldDefinitionDto>> GetFieldsByStepIdAsync(Guid stepId, CancellationToken ct = default)
{
    var allFields = await _repo.GetByEntityTypeAsync(EntityType.ContractStep, ct);
    var stepFields = FilterByStepId(allFields, stepId);

    return stepFields.Select(f => new StepCustomFieldDefinitionDto
    {
        EntityCustomFieldId = f.Id,
        FieldName = f.FieldName,
        FieldType = f.FieldType,
        IsRequired = f.IsRequired
    }).ToList();
}
}