using Application.Dto.ApplicationUser;
using MediatR;

namespace Application.Queries.ApplicationUser.GetAllUsers;

public class GetAllUserQuery : IRequest<IEnumerable<ApplicationUserDto.Response>>
{
}