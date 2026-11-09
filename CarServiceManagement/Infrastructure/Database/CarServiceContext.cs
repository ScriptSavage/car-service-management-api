using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class CarServiceContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
{

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    public CarServiceContext(DbContextOptions<CarServiceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarServiceContext).Assembly);
    }
}