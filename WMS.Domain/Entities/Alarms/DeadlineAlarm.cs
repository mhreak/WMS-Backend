using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Alarms;

public enum AlarmEntityType
{
    Contract = 1,
    ContractStep = 2
}

public class DeadlineAlarm : BaseEntity
{
    [Required]
    public AlarmEntityType EntityType { get; set; }

    // اگه EntityType = ContractStep باشه، این معادل ContractStep.Id (کلید Contract_ContractTypeStep) نیست چون اون کلید ترکیبیه؛
    // پس این‌جا ContractId رو نگه می‌داریم و StepId رو جدا، تا بشه دقیق به همون ردیف اشاره کرد.
    [Required]
    public Guid ContractId { get; set; }

    public Guid? StepId { get; set; } // فقط وقتی EntityType = ContractStep پر می‌شه

    [Required]
    public int DaysBeforeDeadline { get; set; } // مثلاً 20 یا 10

    public bool IsActive { get; set; } = true;
}