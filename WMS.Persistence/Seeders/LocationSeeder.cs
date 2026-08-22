using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities.Locations;
using WMS.Persistence.Context;
using WMS.Persistence.Seeders.Models;

namespace WMS.Persistence.Seeders;

public static class LocationSeeder
{
    private const int IranCountryId = 103;

    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Countries.AnyAsync()
            && await context.Provinces.AnyAsync()
            && await context.Cities.AnyAsync())
            return;

        var dataDir = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

        var countriesFilePath = Path.Combine(dataDir, "countries.json");
        var statesFilePath = Path.Combine(dataDir, "states.json");
        var citiesFilePath = Path.Combine(dataDir, "cities.json");

        if (!File.Exists(countriesFilePath) ||
            !File.Exists(statesFilePath) ||
            !File.Exists(citiesFilePath))
        {
            Console.WriteLine("[LocationSeeder] فایل‌های JSON پیدا نشدند، Seed skipped.");
            return;
        }

        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        List<CountryJson>? countriesJson;
        List<StateJson>? statesJson;
        List<CityJson>? citiesJson;

        try
        {
            countriesJson = JsonSerializer.Deserialize<List<CountryJson>>(
                await File.ReadAllTextAsync(countriesFilePath), jsonOptions);

            statesJson = JsonSerializer.Deserialize<List<StateJson>>(
                await File.ReadAllTextAsync(statesFilePath), jsonOptions);

            citiesJson = JsonSerializer.Deserialize<List<CityJson>>(
                await File.ReadAllTextAsync(citiesFilePath), jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LocationSeeder] خطا در خواندن JSON: {ex.Message}");
            return;
        }

        if (countriesJson is null || statesJson is null || citiesJson is null)
        {
            Console.WriteLine("[LocationSeeder] فایل‌های JSON خالی یا نامعتبر بودند، Seed skipped.");
            return;
        }

        Console.WriteLine($"[LocationSeeder] خونده شد -> Countries: {countriesJson.Count}, States: {statesJson.Count}, Cities: {citiesJson.Count}");

        // فقط ایران
        var iranCountry = countriesJson
            .FirstOrDefault(c => c.Id == IranCountryId || c.Iso2 == "IR" || c.Iso3 == "IRN" || c.Name == "Iran");

        if (iranCountry is null)
        {
            Console.WriteLine("[LocationSeeder] ایران در فایل countries.json پیدا نشد.");
            return;
        }

        var countryId = DeterministicGuid.Create($"Country_{iranCountry.Id}");

        var country = new Country
        {
            Id = countryId,
            Name = GetPersianName(iranCountry.Name, iranCountry.Native, iranCountry.Translations),
            Iso2 = iranCountry.Iso2 ?? string.Empty,
            Iso3 = iranCountry.Iso3 ?? string.Empty,
            PhoneCode = iranCountry.PhoneCode ?? string.Empty,
            Capital = iranCountry.Capital ?? string.Empty,
            Currency = iranCountry.Currency ?? string.Empty,
            Region = iranCountry.Region ?? string.Empty,
            Subregion = iranCountry.Subregion ?? string.Empty,
            Latitude = ParseDecimal(iranCountry.Latitude),
            Longitude = ParseDecimal(iranCountry.Longitude)
        };

        // فقط استان‌های ایران
        var iranStates = statesJson
            .Where(s => s.CountryId == IranCountryId || s.CountryId == iranCountry.Id)
            .ToList();

        var provinceIdMap = iranStates
            .Select(s => s.Id)
            .Distinct()
            .ToDictionary(id => id, id => DeterministicGuid.Create($"Province_{id}"));

        var provinces = iranStates
            .Select(s => new Province
            {
                Id = provinceIdMap[s.Id],
                Name = GetPersianName(s.Name, s.Native, s.Translations),
                CountryId = countryId,
                StateCode = s.StateCode,
                Latitude = ParseDecimal(s.Latitude),
                Longitude = ParseDecimal(s.Longitude)
            })
            .ToList();

        var validProvinceOriginalIds = iranStates.Select(s => s.Id).ToHashSet();

        // فقط شهرهای ایران
        var iranCities = citiesJson
            .Where(c => c.CountryId == IranCountryId || c.CountryId == iranCountry.Id)
            .ToList();

        var cities = new List<City>();
        var skippedCities = 0;

        foreach (var c in iranCities)
        {
            if (validProvinceOriginalIds.Contains(c.StateId))
            {
                cities.Add(new City
                {
                    Id = DeterministicGuid.Create($"City_{c.Id}"),
                    Name = GetPersianName(c.Name, c.Native, c.Translations),
                    ProvinceId = provinceIdMap[c.StateId],
                    CountryId = countryId,
                    Latitude = ParseDecimal(c.Latitude),
                    Longitude = ParseDecimal(c.Longitude)
                });
            }
            else
            {
                skippedCities++;
            }
        }

        if (skippedCities > 0)
            Console.WriteLine($"[LocationSeeder] هشدار: {skippedCities} شهر به‌خاطر ProvinceId نامعتبر رد شد.");

        Console.WriteLine($"[LocationSeeder] آماده‌ی Insert -> Country: 1, Provinces: {provinces.Count}, Cities: {cities.Count}");

        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            if (await context.Cities.AnyAsync())
            {
                await context.Cities.ExecuteDeleteAsync();
                Console.WriteLine("[LocationSeeder] Cities قبلی پاک شد.");
            }

            if (await context.Provinces.AnyAsync())
            {
                await context.Provinces.ExecuteDeleteAsync();
                Console.WriteLine("[LocationSeeder] Provinces قبلی پاک شد.");
            }

            if (await context.Countries.AnyAsync())
            {
                await context.Countries.ExecuteDeleteAsync();
                Console.WriteLine("[LocationSeeder] Countries قبلی پاک شد.");
            }

            await BulkInsertAsync(context, new List<Country> { country });
            await BulkInsertAsync(context, provinces);
            await BulkInsertAsync(context, cities);

            await transaction.CommitAsync();
            Console.WriteLine("[LocationSeeder] Seed با موفقیت تمام شد.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"[LocationSeeder] خطا در Seed: {ex.Message}");
            throw;
        }
    }

    private static string GetPersianName(string? englishName, string? native, JsonElement? translations)
{
    if (translations.HasValue && translations.Value.ValueKind == JsonValueKind.Object)
    {
        if (translations.Value.TryGetProperty("fa", out var faElement) &&
            faElement.ValueKind == JsonValueKind.String)
        {
            var faName = faElement.GetString();
            if (!string.IsNullOrWhiteSpace(faName))
                return faName;
        }
    }

    if (!string.IsNullOrWhiteSpace(native))
        return native;

    return englishName ?? string.Empty;
}

    private static async Task BulkInsertAsync<T>(AppDbContext context, List<T> entities) where T : class
    {
        const int batchSize = 2000;
        context.ChangeTracker.AutoDetectChangesEnabled = false;
        try
        {
            for (var i = 0; i < entities.Count; i += batchSize)
            {
                var batch = entities.Skip(i).Take(batchSize);
                await context.Set<T>().AddRangeAsync(batch);
                await context.SaveChangesAsync();
                context.ChangeTracker.Clear();
            }
        }
        finally
        {
            context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

    private static decimal? ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : null;
    }
}
