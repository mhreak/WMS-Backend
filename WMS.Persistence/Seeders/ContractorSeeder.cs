using Microsoft.EntityFrameworkCore;
using WMS.Persistence.Context;
using WMS.Domain.Entities.Contractors;
using WMS.Domain.Enums;

namespace WMS.Persistence.Seeders;

public static class ContractorSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Contractors.AnyAsync())
            return;

        var iranCountry = await context.Countries.FirstOrDefaultAsync(c => c.Iso2 == "IR");
        if (iranCountry == null)
            return;

        var tehranProvince = await context.Provinces.FirstOrDefaultAsync(p => p.CountryId == iranCountry.Id && p.Name.Contains("تهران"));
        if (tehranProvince == null)
            return;

        var tehranCity = await context.Cities.FirstOrDefaultAsync(c => c.ProvinceId == tehranProvince.Id && c.Name.Contains("تهران"));
        if (tehranCity == null)
            return;

        var isfahanProvince = await context.Provinces.FirstOrDefaultAsync(p => p.CountryId == iranCountry.Id && p.Name.Contains("اصفهان"));
        var isfahanCity = isfahanProvince != null 
            ? await context.Cities.FirstOrDefaultAsync(c => c.ProvinceId == isfahanProvince.Id && c.Name.Contains("اصفهان"))
            : null;

        var contractors = new List<Contractor>
        {
            new Contractor
            {
                Id = Guid.NewGuid(),
                Type = ContractorType.Legal,
                CompanyName = "شرکت فنی مهندسی نوین پیمان",
                FirstName = "علی",
                LastName = "محمدی",
                CityId = tehranCity.Id,
                Mobile1 = "09121234567",
                Phone1 = "02112345678",
                Email = "info@novinpaiman.ir",
                NationalCode = "12345678901",
                EconomicCode = "987654321",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contractor
            {
                Id = Guid.NewGuid(),
                Type = ContractorType.Individual,
                FirstName = "رضا",
                LastName = "احمدی",
                CityId = tehranCity.Id,
                Mobile1 = "09351234567",
                Phone1 = "02187654321",
                Email = "reza.ahmadi@email.com",
                NationalCode = "09876543212",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contractor
            {
                Id = Guid.NewGuid(),
                Type = ContractorType.Legal,
                CompanyName = "گروه صنعتی گسترش سازه",
                FirstName = "محسن",
                LastName = "کریمی",
                CityId = isfahanCity != null ? isfahanCity.Id : tehranCity.Id,
                Mobile1 = "09131234567",
                Phone1 = "03112345678",
                Email = "contact@gostareshsazeh.ir",
                NationalCode = "11122233344",
                EconomicCode = "555666777",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Contractor
            {
                Id = Guid.NewGuid(),
                Type = ContractorType.Legal,
                CompanyName = "شرکت پشتیبانی فنی ایران",
                FirstName = "سارا",
                LastName = "حسینی",
                CityId = tehranCity.Id,
                Mobile1 = "09151234567",
                Email = "sara@technical.ir",
                NationalCode = "44455566677",
                EconomicCode = "888999000",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        await context.Contractors.AddRangeAsync(contractors);
        await context.SaveChangesAsync();

        var categoryIds = await context.ContractorCategories.Select(c => c.Id).ToListAsync();

        if (categoryIds.Count >= 1)
        {
            var contractor1 = await context.Contractors.FindAsync(contractors[0].Id);
            var cat1 = await context.ContractorCategories.FindAsync(categoryIds[0]);
            if (contractor1 != null && cat1 != null)
            {
                contractor1.Categories.Add(cat1);
            }
        }

        if (categoryIds.Count >= 2)
        {
            var contractor1 = await context.Contractors.FindAsync(contractors[0].Id);
            var cat2 = await context.ContractorCategories.FindAsync(categoryIds[1]);
            if (contractor1 != null && cat2 != null)
            {
                contractor1.Categories.Add(cat2);
            }
        }

        if (categoryIds.Count >= 3)
        {
            var contractor3 = await context.Contractors.FindAsync(contractors[2].Id);
            var cat3 = await context.ContractorCategories.FindAsync(categoryIds[2]);
            if (contractor3 != null && cat3 != null)
            {
                contractor3.Categories.Add(cat3);
            }
        }

        await context.SaveChangesAsync();
    }
}
