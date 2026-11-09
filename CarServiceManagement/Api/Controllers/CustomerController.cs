using Application.Queries.Customers.GetAllCustomers;
using Application.Queries.Customers.GetCustomerById;
using MediatR;
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
    public async Task<IActionResult> GetAllCustomers()
    {
        var customersResponse = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(customersResponse);
    }
    
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] long id)
    {
        var customerResponse = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(customerResponse);
    }


}