using Infrastructure.Entities;

namespace Infrastructure.Repositories.CustomerRepo;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> FindCustomerByApplicationUserId(long userId);
}