using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(x => x.VehicleId);
        
        builder.Property(x => x.Brand)
            .IsRequired()
            .HasMaxLength(250);
        
        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(250);
        
        builder.Property(x => x.Color)
            .IsRequired()
            .HasMaxLength(250);
        
        builder.Property(x => x.Vin)
            .IsRequired()
            .HasMaxLength(250);
        
        builder.HasIndex(e=>e.Vin)
            .IsUnique();
        
        builder.Property(e=>e.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.HasIndex(e=>e.RegistrationNumber)
            .IsUnique();
    }
}