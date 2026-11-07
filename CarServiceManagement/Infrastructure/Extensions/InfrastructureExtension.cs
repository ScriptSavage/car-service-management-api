using Infrastructure.Database;
using Infrastructure.Helpers;
using Infrastructure.Repositories.ApplicationUserRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The ConnectionString property has not been initialized");
        }

        services.AddDbContext<CarServiceContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
    
        services.AddScoped<IApplicationUserRepository,ApplicationUserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}