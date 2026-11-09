using Application.Commands.ApplicationUser.RegisterNewClient;
using Application.Dto.ApplicationUser;
using Application.Queries.ApplicationUser.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/application-users")]
public class ApplicationUserController : ControllerBase
{
    private readonly IMediator _mediator;
    public ApplicationUserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUserQuery());
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser([FromBody] ApplicationUserDto.RegisterNewUserDto request ,CancellationToken cancellationToken)
    {
        var userIdResponse = await _mediator.Send(new RegisterNewCustomerCommand(request), cancellationToken);
        return Ok(userIdResponse);
    }


}