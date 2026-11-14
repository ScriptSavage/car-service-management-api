using Application.Dto.Vehicle;
using Infrastructure.Repositories.CustomerRepo;
using Infrastructure.Repositories.VehicleRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries.Customers.GetCustomerVehicles;

public class GetCustomerVehiclesQueryHandler : IRequestHandler<GetCustomerVehiclesQuery,IEnumerable<VehicleDto.Response>>
{
    private readonly UserManager<Infrastructure.Entities.ApplicationUser> _userManager;
    private readonly ICustomerRepository _customerRepository;
    private readonly IVehicleRepository _vehicleRepository;

    public GetCustomerVehiclesQueryHandler(
        UserManager<Infrastructure.Entities.ApplicationUser> userManager, 
        ICustomerRepository customerRepository, 
        IVehicleRepository vehicleRepository)
    {
        _userManager = userManager;
        _vehicleRepository = vehicleRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<VehicleDto.Response>> Handle(GetCustomerVehiclesQuery request, 
        CancellationToken cancellationToken)
    {
        var user =  await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var customer = await _customerRepository.FindCustomerByApplicationUserId(user.Id);
        if (customer is null)
        {
            throw new UnauthorizedAccessException();
        }

        var vehicles = await _vehicleRepository.GetVehiclesByCustomerIdAsync(customer.CustomerId);

        return vehicles.Select(e => new VehicleDto.Response(e.VehicleId,
            e.Brand,
            e.Model,
            e.ProductionYear,
            e.Vin,
            e.RegistrationNumber,
            e.Color,
            e.MileAge));
    }
}