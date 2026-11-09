using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Helpers;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
}