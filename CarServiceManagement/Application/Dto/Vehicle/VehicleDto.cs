namespace Application.Dto.Vehicle;

public static class VehicleDto
{
    public record Response(
        long Id,
        string Brand,
        string Model,
        int ProductionYear,
        string VinNumber,
        string PlateNumber,
        string Color,
        int Mileage);
    
    public record CreateNewVehicle(
        string Brand,
        string Model,
        int ProductionYear,
        string VinNumber,
        string PlateNumber,
        string Color,
        int Mileage);
}