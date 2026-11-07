using Infrastructure.Database;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.ApplicationUserRepo;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly CarServiceContext _context;

    public ApplicationUserRepository(CarServiceContext context)
    {
        _context = context;
    }
    
}