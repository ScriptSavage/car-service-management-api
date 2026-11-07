using Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Helpers;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarServiceContext _context;

    public UnitOfWork(CarServiceContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
       return await _context.Database.BeginTransactionAsync();
    }
}