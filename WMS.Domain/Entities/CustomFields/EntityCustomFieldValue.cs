using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.Domain.Entities.CustomFields;

/// <summary>
/// جدول واسط: مقدار یک فیلد سفارشی برای یک entity مشخص
/// </summary>
public class EntityCustomFieldValue
{
    [Required]
    public Guid EntityCustomFieldId { get; set; }

    [ForeignKey(nameof(EntityCustomFieldId))]
    public virtual EntityCustomField EntityCustomField { get; set; } = null!;

    /// <summary>
    /// Id همان entity (Contract / Contractor / Statement و ...)
    /// </summary>
    [Required]
    public Guid EntityId { get; set; }

    [MaxLength(1000)]
    public string? Value { get; set; }
}