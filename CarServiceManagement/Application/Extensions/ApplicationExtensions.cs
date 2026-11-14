using Application.Commands.ApplicationUser.RegisterNewClient;
using Application.Commands.Vehicle.CreateVehicle;
using Application.Queries.ApplicationUser.GetAllUsers;
using Application.Queries.Customers.GetCustomerById;
using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<CarServiceContext>();


        services.AddMediatR(typeof(RegisterNewCustomerCommand));
        services.AddMediatR(typeof(CreateVehicleCommand));
        
        services.AddMediatR(typeof(GetAllUserQuery));
        services.AddMediatR(typeof(GetCustomerByIdQuery));

        
    }
        
}