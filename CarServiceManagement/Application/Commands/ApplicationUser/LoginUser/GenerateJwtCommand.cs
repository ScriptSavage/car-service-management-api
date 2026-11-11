using Application.Dto.ApplicationUser;
using MediatR;

namespace Application.Commands.ApplicationUser.LoginUser;

public record GenerateJwtCommand(ApplicationUserDto.LoginRequest LoginRequest) : IRequest<string>
{
}