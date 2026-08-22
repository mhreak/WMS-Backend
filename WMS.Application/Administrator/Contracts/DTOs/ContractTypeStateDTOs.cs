public class CreateContractTypeStateRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateContractTypeStateRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class ContractTypeStateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}