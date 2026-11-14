using Application.Exceptions;
using Application.Queries.Customers.GetCustomerById;
using Infrastructure.Entities;
using Infrastructure.Repositories.CustomerRepo;
using Moq;

namespace Tests.CustomerTests.Queries;

public class GetCustomerByIdQueryTests
{
    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomer()
    {
        var mockedCustomerRepository = new Mock<ICustomerRepository>();

        var validCustomer = new Customer()
        {
            CustomerId = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
        };
        
        mockedCustomerRepository
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(validCustomer);
        
        
        var handler = new GetCustomerByIdQueryHandler(mockedCustomerRepository.Object);
        
        var result = await handler.Handle(new GetCustomerByIdQuery(1), CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(validCustomer.CustomerId, result.CustomerId);
        
        mockedCustomerRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<long>()), Times.Once);
    }


    [Fact]
    public async Task GetCustomerById_CustomerDoesNotExists_ShouldThrowException()
    {
        var mockedCustomerRepository = new Mock<ICustomerRepository>();

        mockedCustomerRepository
            .Setup(e => e.GetByIdAsync(1))
            .ReturnsAsync((Customer?)null);


        var handler = new GetCustomerByIdQueryHandler(mockedCustomerRepository.Object);
        
        var exception = await Assert.ThrowsAsync<NotFoundException>( ()=> handler.Handle(new GetCustomerByIdQuery(1), CancellationToken.None));
        
        
        mockedCustomerRepository
            .Verify(repo => repo.GetByIdAsync(It.IsAny<long>()), Times.Once);
        
        Assert.Equal("Customer not found", exception.Message);
    }


}