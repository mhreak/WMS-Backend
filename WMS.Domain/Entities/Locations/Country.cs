using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Locations;

public class Country : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(2)]
    public string Iso2 { get; set; } = string.Empty;

    [Required]
    [MaxLength(3)]
    public string Iso3 { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string PhoneCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Capital { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Currency { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string CurrencySymbol { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Region { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Subregion { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}
