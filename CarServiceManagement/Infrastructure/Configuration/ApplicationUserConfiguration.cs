using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.HasKey(e => e.Id);
        
        
        builder.Property(l=>l.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(l => l.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(250)
            .IsRequired();
        
        builder.HasIndex(e=>e.Email).IsUnique();
        
        builder.HasIndex(e=>e.UserName)
            .IsUnique();
        
        
        builder.HasOne(e=>e.Customer)
            .WithOne(e=>e.ApplicationUser)
            .HasForeignKey<Customer>(e => e.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}