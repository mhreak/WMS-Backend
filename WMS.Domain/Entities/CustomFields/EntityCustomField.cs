using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.CustomFields;

public class EntityCustomField : BaseEntity
{
    [Required]
    public EntityType EntityType { get; set; }

    [Required]
    [MaxLength(150)]
    public string FieldName { get; set; } = string.Empty;

    [Required]
    public CustomFieldType FieldType { get; set; }

    public string? Config { get; set; }

    public bool IsActive { get; set; } = true;

    /// <summary>
    /// اگر true باشد، پر کردن این فیلد برای entity الزامی است
    /// </summary>
    public bool IsRequired { get; set; } = false;

    public virtual ICollection<EntityCustomFieldValue> Values { get; set; }
        = new List<EntityCustomFieldValue>();
}