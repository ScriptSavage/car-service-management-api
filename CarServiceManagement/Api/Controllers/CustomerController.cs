using System.Security.Claims;
using Application.Queries.Customers.GetAllCustomers;
using Application.Queries.Customers.GetCustomerById;
using Application.Queries.Customers.GetCustomerVehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/customers")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customersResponse = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(customersResponse);
    }
    
    
    [HttpGet("{id:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCustomerById([FromRoute] long id)
    {
        var customerResponse = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(customerResponse);
    }

    [HttpGet("vehicles")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetCustomerVehicles()
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (user is null)
        {
            return  Unauthorized();
        }
        
        var userVehicles = await _mediator.Send(new GetCustomerVehiclesQuery(user));
        return Ok(userVehicles);
    }


}