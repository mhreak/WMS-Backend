namespace WMS.Application.Administrator.Statements.DTOs;

public class CreateExtraOrDeductionTypeRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsExtra { get; set; }
}

public class UpdateExtraOrDeductionTypeRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsExtra { get; set; }
}

public class ExtraOrDeductionTypeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsExtra { get; set; }
    public DateTime CreatedAt { get; set; }
}