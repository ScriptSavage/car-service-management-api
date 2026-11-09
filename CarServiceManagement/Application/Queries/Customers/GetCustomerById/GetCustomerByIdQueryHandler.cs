using Application.Dto.Customer;
using Application.Exceptions;
using Infrastructure.Repositories.CustomerRepo;
using MediatR;

namespace Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery,CustomerDto.Response>
{
    private readonly ICustomerRepository  _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto.Response> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            throw new NotFoundException("Customer not found");
        }

        return new CustomerDto.Response(customer.CustomerId,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.PhoneNumber);
    }
}