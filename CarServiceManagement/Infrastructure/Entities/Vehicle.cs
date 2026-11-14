namespace Infrastructure.Entities;

public class Vehicle
{
    public long VehicleId { get; set; }
    public required string Brand { get; set; }
    public string? Model { get; set; }
    public int ProductionYear { get; set; }
    public required string Vin { get; set; }
    public string? Color { get; set; }
    public string? RegistrationNumber { get; set; }
    public int MileAge { get; set; }

    public required Customer Customer { get; set; }
    public long CustomerId { get; set; }
}