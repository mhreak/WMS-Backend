namespace WMS.Application.Administrator.Contracts.DTOs;

public class IncompleteStepCustomFieldsError
{
    public Guid StepId { get; set; }
    public string StepTitle { get; set; } = string.Empty;
    public List<IncompleteCustomFieldItem> Fields { get; set; } = new();
}

public class IncompleteCustomFieldItem
{
    public Guid EntityCustomFieldId { get; set; }
    public string FieldName { get; set; } = string.Empty;
}