using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;
using WMS.Domain.Entities.Locations;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities.Contractors;

public class Contractor : BaseEntity
{
    [Required]
    public ContractorType Type { get; set; }

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(200)]
    public string? CompanyName { get; set; }

    public Guid? CityId { get; set; }

    [ForeignKey(nameof(CityId))]
    public virtual City? City { get; set; }

    [MaxLength(20)]
    public string? Mobile1 { get; set; }

    [MaxLength(20)]
    public string? Mobile2 { get; set; }

    [MaxLength(20)]
    public string? Phone1 { get; set; }

    [MaxLength(20)]
    public string? Phone2 { get; set; }

    [MaxLength(150)]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(20)]
    public string? NationalCode { get; set; }

    [MaxLength(20)]
    public string? EconomicCode { get; set; }

    public virtual ICollection<ContractorCategory> Categories { get; set; } = new List<ContractorCategory>();
}
