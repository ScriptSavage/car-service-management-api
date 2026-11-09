using Application.Dto.ApplicationUser;
using MediatR;

namespace Application.Commands.ApplicationUser.RegisterNewClient;

public record RegisterNewCustomerCommand(ApplicationUserDto.RegisterNewUserDto RegisterNewUserDto) : IRequest<long>;