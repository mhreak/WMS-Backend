using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Locations;

public class City : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    [Required]
    public Guid ProvinceId { get; set; }

    [ForeignKey(nameof(ProvinceId))]
    public virtual Province Province { get; set; } = null!;

    [Required]
    public Guid CountryId { get; set; }

    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = null!;
}
