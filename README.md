# WMS — .NET Backend Template

Clean layered template with **Identity**, **OTP + JWT**, **Admin auth**, **File upload**, **Location seed**, and **Localization**.

## Projects
- `WMS.Domain` — entities (User, Admin, FileAsset, Country/Province/City)
- `WMS.Application` — services, DTOs, interfaces, validators
- `WMS.Infrastructure` — JWT, Redis OTP/RefreshToken, Fake SMS/Email, Localization
- `WMS.Persistence` — EF Core, repositories, seeders
- `WMS.API` — controllers, DI, middleware

## Roles
- `Admin` — admin area (`/api/v1/admin/...`)
- `User` — generic auth (`/api/v1/auth/...`)

## Configure
Edit `WMS.API/appsettings.json`:
- `ConnectionStrings:DefaultConnection` (SQL Server)
- `ConnectionStrings:Redis`
- `JwtSettings`, `Otp`, `Tokens`, `GoogleAuth` (optional), `Storage`

## First run
```bash
dotnet restore
dotnet ef migrations add InitialCreate -p WMS.Persistence -s WMS.API
dotnet run --project WMS.API
```

## Default Admin (from AdminSeeder)
- Username: `admin`
- Password: `Admin@123`

## Location seed data
Place `countries.json`, `states.json`, `cities.json` under:
`WMS.Persistence/Seeders/Data/`
(and ensure they are copied to output). If missing, seeder skips safely.

## Extending for a new domain
1. Add domain entities under `WMS.Domain`.
2. Extend `User` with optional FKs (e.g. `DoctorId`) following the existing `AdminId` pattern.
3. Add role constants and wire them in new auth services via `RoleAuthCore`.
4. Add DbSets + configurations in Persistence.
5. Add area/controllers as needed.
