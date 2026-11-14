using Application.Dto.Vehicle;
using MediatR;

namespace Application.Commands.Vehicle.CreateVehicle;

public record CreateVehicleCommand(string UserId,VehicleDto.CreateNewVehicle CreateNewVehicle) : IRequest<long>
{
}