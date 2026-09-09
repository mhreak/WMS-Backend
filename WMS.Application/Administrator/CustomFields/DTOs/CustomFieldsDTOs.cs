using WMS.Domain.Enums;

public class CreateEntityCustomFieldRequest
{
    public EntityType EntityType { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public CustomFieldType FieldType { get; set; }
    public string? Config { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRequired { get; set; } = false;   // جدید
}

public class UpdateEntityCustomFieldRequest
{
    public string FieldName { get; set; } = string.Empty;
    public CustomFieldType FieldType { get; set; }
    public string? Config { get; set; }
    public bool IsActive { get; set; }
    public bool IsRequired { get; set; }             // جدید
}

public class EntityCustomFieldDto
{
    public Guid Id { get; set; }
    public EntityType EntityType { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public CustomFieldType FieldType { get; set; }
    public string? Config { get; set; }
    public bool IsActive { get; set; }
    public bool IsRequired { get; set; }             // جدید
    public DateTime CreatedAt { get; set; }
}