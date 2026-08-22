using System.Text.Json;
using System.Text.Json.Serialization;

namespace WMS.Persistence.Seeders.Models;

public class CityJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("state_id")]
    public int StateId { get; set; }

    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }

    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Native { get; set; }
    public JsonElement? Translations { get; set; }
}
