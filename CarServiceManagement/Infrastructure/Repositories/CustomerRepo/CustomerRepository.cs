using Infrastructure.Database;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.CustomerRepo;

public class CustomerRepository : ICustomerRepository
{
    private readonly CarServiceContext  _carServiceContext;

    public CustomerRepository(CarServiceContext carServiceContext)
    {
        _carServiceContext = carServiceContext;
    }

    public async Task AddAsync(Customer entity)
    {
        await _carServiceContext.Customers.AddAsync(entity);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _carServiceContext.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(long id)
    {
        return await _carServiceContext.Customers.FindAsync(id);
    }

    public async Task<Customer?> FindCustomerByApplicationUserId(long userId)
    {
        return await  _carServiceContext.Customers
            .FirstOrDefaultAsync(e=>e.ApplicationUserId == userId);
    }
}