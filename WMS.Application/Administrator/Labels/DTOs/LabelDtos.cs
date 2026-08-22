namespace WMS.Application.Administrator.Labels.DTOs;
using WMS.Domain.Enums;
public class CreateLabelRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public bool IsActive { get; set; } = true;
    public EntityType EntityType { get; set; }
}

public class UpdateLabelRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public bool IsActive { get; set; }
    public EntityType EntityType { get; set; }
}

public class LabelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public bool IsActive { get; set; }
    public EntityType EntityType { get; set; }
    public string EntityTypeName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class LabelFilterRequest
{
    public string? Search { get; set; }
    public EntityType? EntityType { get; set; }
    public bool? IsActive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}