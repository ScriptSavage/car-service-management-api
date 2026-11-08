using Infrastructure.Entities;

namespace Infrastructure.Repositories.ApplicationUserRepo;

public interface IApplicationUserRepository 
{
    Task<bool> DoesUserNameAlreadyExistAsync(string userName);
    Task<bool> DoesEmailAlreadyExistAsync(string email);
}