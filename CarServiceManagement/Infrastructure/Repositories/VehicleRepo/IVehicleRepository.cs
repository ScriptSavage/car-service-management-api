using Infrastructure.Entities;

namespace Infrastructure.Repositories.VehicleRepo;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> GetVehiclesByCustomerIdAsync(long customerId);
}