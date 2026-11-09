using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e=>e.CustomerId);
        
        builder.Property(e => e.CustomerId).IsRequired();
        
        builder.Property(e => e.Email)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(l=>l.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(l => l.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(250)
            .IsRequired();
        
    }
}