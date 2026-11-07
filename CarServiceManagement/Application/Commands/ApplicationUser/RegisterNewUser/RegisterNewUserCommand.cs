using Application.Dto.ApplicationUser;
using MediatR;

namespace Application.Commands.ApplicationUser.RegisterNewUser;

public record RegisterNewUserCommand(ApplicationUserDto.RegisterNewUserDto RegisterNewUserDto) : IRequest<long>;