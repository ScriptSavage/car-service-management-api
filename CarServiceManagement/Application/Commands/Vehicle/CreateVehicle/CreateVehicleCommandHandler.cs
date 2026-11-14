using Infrastructure.Helpers;
using Infrastructure.Repositories.CustomerRepo;
using Infrastructure.Repositories.VehicleRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Vehicle.CreateVehicle;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand,long>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Infrastructure.Entities.ApplicationUser>  _userManager;

    public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository, 
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork, 
        UserManager<Infrastructure.Entities.ApplicationUser> userManager)
    {
        _vehicleRepository = vehicleRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<long> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new UnauthorizedAccessException("XDD");
        }

        var customer = await _customerRepository.FindCustomerByApplicationUserId(user.Id);

        if (customer is null)
        {
            throw new UnauthorizedAccessException("DUPA");
        }

        var newVehicle = new Infrastructure.Entities.Vehicle
        {
            Brand = request.CreateNewVehicle.Brand,
            Model = request.CreateNewVehicle.Model,
            ProductionYear = request.CreateNewVehicle.ProductionYear,
            Vin = request.CreateNewVehicle.VinNumber,
            RegistrationNumber = request.CreateNewVehicle.PlateNumber,
            MileAge = request.CreateNewVehicle.Mileage,
            Color = request.CreateNewVehicle.Color,
            Customer = customer
        };
        
        await _vehicleRepository.AddAsync(newVehicle);
        await _unitOfWork.SaveChangesAsync();
        
        return newVehicle.VehicleId;
    }
}