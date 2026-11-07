using Application.Commands.ApplicationUser.RegisterNewUser;
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser(RegisterNewUserCommand request,CancellationToken cancellationToken)
    {
        var userIdResponse = await _mediator.Send(request, cancellationToken);
        return Ok(userIdResponse);
    }


}