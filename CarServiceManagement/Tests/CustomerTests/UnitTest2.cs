using Application.Dto.Customer;
using Application.Exceptions;
using Application.Queries.Customers.GetAllCustomers;
using Application.Queries.Customers.GetCustomerById;
using Infrastructure.Entities;
using Infrastructure.Repositories.CustomerRepo;
using Moq;

namespace Tests.CustomerTests;

public class UnitTest2
{
    [Fact]
    public async Task GetAllCustomers_ShouldReturnAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new()
            {
                CustomerId = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan@example.com",
                PhoneNumber = "123456789"
            },
            new()
            {
                CustomerId = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna@example.com",
                PhoneNumber = "987654321"
            }
        };

        var repositoryMock = new Mock<ICustomerRepository>();
        repositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(customers);

        var handler = new GetAllCustomersQueryHandler(repositoryMock.Object);

        // Act
        var result = (await handler.Handle(new GetAllCustomersQuery(), CancellationToken.None)).ToArray();

        // Assert
        var expected = new[]
        {
            new CustomerDto.Response(
                1, "Jan", "Kowalski", "jan@example.com", "123456789"),
            new CustomerDto.Response(
                2, "Anna", "Nowak", "anna@example.com", "987654321")
        };

        Assert.Equal(expected, result);
        repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

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
    public async Task GetCustomerById_CustomerDoesNotExists_ShouldThorException()
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