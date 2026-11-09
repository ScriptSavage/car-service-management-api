using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
   Task AddAsync(T entity);
   
   Task<IEnumerable<T>> GetAllAsync();
   
   Task<Customer?> GetByIdAsync(long id);
}