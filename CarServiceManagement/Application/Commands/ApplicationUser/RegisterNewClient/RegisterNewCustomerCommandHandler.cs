using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Repositories.CustomerRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.ApplicationUser.RegisterNewClient;

public class RegisterNewCustomerCommandHandler : IRequestHandler<RegisterNewCustomerCommand, long>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Infrastructure.Entities.ApplicationUser> _passwordHasher;
    private readonly UserManager<Infrastructure.Entities.ApplicationUser> _userManager;
    private readonly ICustomerRepository _customerRepository;
  

    public RegisterNewCustomerCommandHandler(IUnitOfWork unitOfWork,
        ICustomerRepository  customerRepository,
        IPasswordHasher<Infrastructure.Entities.ApplicationUser> passwordHasher,
        UserManager<Infrastructure.Entities.ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _userManager = userManager;
    }


    public async Task<long> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
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

            var newCustomer = new Customer
            {
                PhoneNumber = request.RegisterNewUserDto.PhoneNumber,
                FirstName = request.RegisterNewUserDto.FirstName,
                LastName = request.RegisterNewUserDto.LastName,
                Email = request.RegisterNewUserDto.Email,
                ApplicationUserId = newApplicationUser.Id,
                ApplicationUser = newApplicationUser
            };
            


            if (request.RegisterNewUserDto.Password != request.RegisterNewUserDto.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match");
            }
            
            var hashPassword = _passwordHasher.HashPassword(newApplicationUser,request.RegisterNewUserDto.Password);
            newApplicationUser.PasswordHash =  hashPassword;

            await _userManager.CreateAsync(newApplicationUser);
            await _customerRepository.AddAsync(newCustomer);
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