using Infrastructure.Database;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ApplicationUserRepo;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly CarServiceContext _context;

    public ApplicationUserRepository(CarServiceContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesUserNameAlreadyExistAsync(string userName)
    {
        return await _context.ApplicationUsers.AnyAsync(e => e.UserName == userName);
    }

    public async Task<bool> DoesEmailAlreadyExistAsync(string email)
    {
        return await _context.ApplicationUsers.AnyAsync(e => e.Email == email);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        return await _context.ApplicationUsers.ToListAsync();
    }
}