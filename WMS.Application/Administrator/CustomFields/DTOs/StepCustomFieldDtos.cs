namespace WMS.Application.Administrator.CustomFields.DTOs;

public class StepCustomFieldDto
{
    public Guid EntityCustomFieldId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public Domain.Enums.CustomFieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
    public string? Value { get; set; }
}

public class SetStepCustomFieldsRequest
{
    public List<StepCustomFieldItemRequest> Values { get; set; } = new();
}

public class StepCustomFieldItemRequest
{
    public Guid EntityCustomFieldId { get; set; }
    public string? Value { get; set; }
}