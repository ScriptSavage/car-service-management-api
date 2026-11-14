namespace Infrastructure.Entities;

public class Customer
{
    public long CustomerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }


    public ApplicationUser? ApplicationUser { get; set; }
    public long? ApplicationUserId { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = [];
}