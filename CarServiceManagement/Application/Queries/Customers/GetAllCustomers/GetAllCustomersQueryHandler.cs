using Application.Dto.Customer;
using Infrastructure.Repositories.CustomerRepo;
using MediatR;

namespace Application.Queries.Customers.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery,IEnumerable<CustomerDto.Response>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDto.Response>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync();

        return customers.Select(e => new CustomerDto.Response(e.CustomerId,
            e.FirstName,
            e.LastName,
            e.Email,
            e.PhoneNumber));
    }
}