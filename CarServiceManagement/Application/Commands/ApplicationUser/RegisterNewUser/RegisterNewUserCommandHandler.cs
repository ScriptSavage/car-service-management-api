using Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.ApplicationUser.RegisterNewUser;

public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, long>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Infrastructure.Entities.ApplicationUser> _passwordHasher;
    private readonly UserManager<Infrastructure.Entities.ApplicationUser> _userManager;
  

    public RegisterNewUserCommandHandler(IUnitOfWork unitOfWork,
        IPasswordHasher<Infrastructure.Entities.ApplicationUser> passwordHasher,
        UserManager<Infrastructure.Entities.ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _userManager = userManager;
    }


    public async Task<long> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var newApplicationUser = new Infrastructure.Entities.ApplicationUser
            {
                UserName = request.RegisterNewUserDto.UserName,
                FirstName = request.RegisterNewUserDto.FirstName,
                LastName = request.RegisterNewUserDto.LastName,
                Email = request.RegisterNewUserDto.Email,
                PhoneNumber = request.RegisterNewUserDto.PhoneNumber
            };


            if (request.RegisterNewUserDto.Password != request.RegisterNewUserDto.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match");
            }
            
            var hashPassword = _passwordHasher.HashPassword(newApplicationUser,request.RegisterNewUserDto.Password);
            newApplicationUser.PasswordHash =  hashPassword;

            await _userManager.CreateAsync(newApplicationUser);
            await _userManager.AddToRoleAsync(newApplicationUser, "User");
            
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);
            
            return newApplicationUser.Id;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            Console.WriteLine(e.Message);
            throw;
        }
        
        
    }
}