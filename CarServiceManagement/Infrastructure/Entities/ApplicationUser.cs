using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? LastPasswordChangedAt { get; set; }

    public Customer? Customer { get; set; }
}