using System.Security.Claims;
using Application.Commands.Vehicle.CreateVehicle;
using Application.Dto.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateVehicle([FromBody] VehicleDto.CreateNewVehicle request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new CreateVehicleCommand(userId,request));

        return Created("api/vehicles", result);
    }
}