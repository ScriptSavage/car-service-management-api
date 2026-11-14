using Infrastructure.Database;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.VehicleRepo;

public class VehicleRepository : IVehicleRepository 
{
    private readonly CarServiceContext _context;
    public VehicleRepository(CarServiceContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Vehicle entity)
    {
        await _context.Vehicles.AddAsync(entity);
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(long id)
    {
        return await _context.Vehicles.FindAsync(id);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByCustomerIdAsync(long customerId)
    {
        return await _context.Vehicles
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }
}