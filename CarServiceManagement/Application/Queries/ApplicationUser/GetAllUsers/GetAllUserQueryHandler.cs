using Application.Dto.ApplicationUser;
using Infrastructure.Repositories.ApplicationUserRepo;
using MediatR;

namespace Application.Queries.ApplicationUser.GetAllUsers;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<ApplicationUserDto.Response>>
{
    private readonly IApplicationUserRepository _userRepository;
    
    public GetAllUserQueryHandler(IApplicationUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ApplicationUserDto.Response>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers();

        return users.Select(e => new ApplicationUserDto.Response(e.Id,
            e.UserName,
            e.FirstName,
            e.LastName,
            e.Email,
            e.PhoneNumber));
    }
}