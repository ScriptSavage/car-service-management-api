using Application.Dto.Vehicle;
using MediatR;

namespace Application.Queries.Customers.GetCustomerVehicles;

public record GetCustomerVehiclesQuery(string UserId) : IRequest<IEnumerable<VehicleDto.Response>>
{
}