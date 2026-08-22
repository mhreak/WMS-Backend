// WMS.Application/Administrator/Contractors/DTOs/ContractorDtos.cs
using WMS.Domain.Enums;

namespace WMS.Application.Administrator.Contractors.DTOs;

// ==================== Request DTOs ====================

public class CreateContractorRequest
{
    public ContractorType Type { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }

    public Guid? CityId { get; set; }

    public string? Mobile1 { get; set; }
    public string? Mobile2 { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public string? NationalCode { get; set; }     // کد ملی
    public string? EconomicCode { get; set; }     // کد اقتصادی

    public List<Guid>? CategoryIds { get; set; }  // اگر هنوز Category جدا داری
    public List<Guid>? LabelIds { get; set; }     // لیبل‌های عمومی (EntityType = Contractor)
}

public class UpdateContractorRequest
{
    public ContractorType Type { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }

    public Guid? CityId { get; set; }

    public string? Mobile1 { get; set; }
    public string? Mobile2 { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public string? NationalCode { get; set; }
    public string? EconomicCode { get; set; }

    public List<Guid>? CategoryIds { get; set; }
    public List<Guid>? LabelIds { get; set; }
}

public class ContractorFilterRequest
{
    public string? Search { get; set; }
    public ContractorType? Type { get; set; }
    public bool? IsActive { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? CityId { get; set; }
    public List<Guid>? CategoryIds { get; set; }
    public List<Guid>? LabelIds { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

// ==================== Response DTOs ====================

public class ContractorListItemDto
{
    public Guid Id { get; set; }
    public ContractorType Type { get; set; }
    public string TypeName { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? DisplayName { get; set; }

    public string? Mobile1 { get; set; }
    public string? Mobile2 { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }

    public Guid? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public Guid? CityId { get; set; }
    public string? CityName { get; set; }

    public List<LookupItemDto> Categories { get; set; } = new();
    public List<LookupItemDto> Labels { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}

public class ContractorDetailDto
{
    public Guid Id { get; set; }
    public ContractorType Type { get; set; }
    public string TypeName { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }

    public Guid? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public Guid? CityId { get; set; }
    public string? CityName { get; set; }

    public string? Mobile1 { get; set; }
    public string? Mobile2 { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public string? NationalCode { get; set; }
    public string? EconomicCode { get; set; }

    public List<LookupItemDto> Categories { get; set; } = new();
    public List<LookupItemDto> Labels { get; set; } = new();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}


public class LookupItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
}

public class CreateLookupRequest
{
    public string Name { get; set; } = string.Empty;
 
    public string? Color { get; set; }
  
    public bool IsActive { get; set; } = true;
}

public class UpdateLookupRequest
{
    public string Name { get; set; } = string.Empty;
        public string? Color { get; set; }

    public bool IsActive { get; set; }
}