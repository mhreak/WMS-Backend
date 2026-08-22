using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WMS.Domain.Entities.Users;

public class User : IdentityUser<Guid>
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
