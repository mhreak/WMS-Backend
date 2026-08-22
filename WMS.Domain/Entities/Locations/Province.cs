using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Common;

namespace WMS.Domain.Entities.Locations;

public class Province : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? StateCode { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    [Required]
    public Guid CountryId { get; set; }

    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
