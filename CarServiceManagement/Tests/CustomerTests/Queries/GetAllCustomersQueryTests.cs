using Application.Dto.Customer;
using Application.Queries.Customers.GetAllCustomers;
using Infrastructure.Entities;
using Infrastructure.Repositories.CustomerRepo;
using Moq;

namespace Tests.CustomerTests.Queries;

public class GetAllCustomersQueryTests
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
}