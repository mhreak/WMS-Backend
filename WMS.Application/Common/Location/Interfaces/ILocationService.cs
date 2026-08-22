using WMS.Application.Common.Location.DTOs;

namespace WMS.Application.Common.Location.Interfaces;

public interface ILocationService
{
    Task<IReadOnlyList<CountryDto>> GetCountriesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<ProvinceDto>> GetProvincesAsync(Guid countryId, CancellationToken ct = default);
    Task<IReadOnlyList<CityDto>> GetCitiesByProvinceAsync(Guid provinceId, CancellationToken ct = default);
    Task<IReadOnlyList<CityDto>> GetCitiesByCountryAsync(Guid countryId, CancellationToken ct = default);
}
