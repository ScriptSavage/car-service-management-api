using Application.Commands.Vehicle.CreateVehicle;
using Application.Dto.Vehicle;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Repositories.CustomerRepo;
using Infrastructure.Repositories.VehicleRepo;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.CustomerTests.Commands;

public class CreateVehicleCommandTests
{
    [Fact]
    public async Task UserDoesNotExist_ShouldThrowUserDoesNotExistException()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

        var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        var mockCustomerRepo = new Mock<ICustomerRepository>();
        var mockVehicleRepo = new Mock<IVehicleRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        userManagerMock
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(null as ApplicationUser);
        
        
        var commandHandler = new CreateVehicleCommandHandler(
            mockVehicleRepo.Object,
            mockCustomerRepo.Object,
            mockUnitOfWork.Object,
            userManagerMock.Object);

        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>( ()=>
            commandHandler.Handle(new CreateVehicleCommand(It.IsAny<string>(), It.IsAny<VehicleDto.CreateNewVehicle>()), CancellationToken.None));
        
        mockVehicleRepo
            .Verify(e=>e.AddAsync(It.IsAny<Vehicle>()), Times.Never);
        
        mockCustomerRepo
            .Verify(e=>e.FindCustomerByApplicationUserId(It.IsAny<long>()), Times.Never);
        
        mockUnitOfWork
            .Verify(e=>e.SaveChangesAsync(), Times.Never);
    }


[Fact]
public async Task UserExists_ShouldCreateVehicle()
{
    var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

    var userManagerMock = new Mock<UserManager<ApplicationUser>>(
        userStoreMock.Object, null, null, null, null, null,null, null, null);

    var mockCustomerRepo = new Mock<ICustomerRepository>();
    var mockVehicleRepo = new Mock<IVehicleRepository>();
    var mockUnitOfWork = new Mock<IUnitOfWork>();

    var appUser = new ApplicationUser
    {
        Id = 123,
        FirstName = "Test1",
        LastName = "Test12"
    };

    var customer = new Customer
    {
        CustomerId = 99,
        FirstName = "customer1",
        LastName = "customer12",
        ApplicationUserId = appUser.Id,
        ApplicationUser = appUser
    };

    var vehicleDto = new VehicleDto.CreateNewVehicle(
        "Brand","Model",2000,"ValidVin","ValidPlateNumber",
        "ValidColor",1234
    );

    userManagerMock
        .Setup(x => x.FindByIdAsync("123"))
        .ReturnsAsync(appUser);

    mockCustomerRepo
        .Setup(x => x.FindCustomerByApplicationUserId(123))
        .ReturnsAsync(customer);

    mockVehicleRepo
        .Setup(x => x.AddAsync(It.IsAny<Vehicle>()));

    mockUnitOfWork
        .Setup(x => x.SaveChangesAsync());

    var handler = new CreateVehicleCommandHandler(
        mockVehicleRepo.Object,
        mockCustomerRepo.Object,
        mockUnitOfWork.Object,
        userManagerMock.Object);

    var command = new CreateVehicleCommand("123", vehicleDto);

    await handler.Handle(command, CancellationToken.None);
    
    userManagerMock
        .Verify(x => x.FindByIdAsync("123"), Times.Once);

    mockCustomerRepo
        .Verify(x => x.FindCustomerByApplicationUserId(123), Times.Once);

    mockVehicleRepo
        .Verify(x => x.AddAsync(It.IsAny<Vehicle>()), Times.Once);

    mockUnitOfWork
        .Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}