using Application.Dto.Customer;
using MediatR;

namespace Application.Queries.Customers.GetCustomerById;

public record GetCustomerByIdQuery(long CustomerId) : IRequest<CustomerDto.Response>
{
    
}