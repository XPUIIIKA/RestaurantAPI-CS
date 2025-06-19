using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Configurations;

public class UserConfig :  IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(16);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(16);
        
        builder.Property(u => u.HashPassword)
            .IsRequired()
            .HasMaxLength(64);
        
        builder.Property(u => u.Login)
            .HasMaxLength(16)
            .IsRequired();
        
        builder.HasIndex(u => u.Login)
            .IsUnique();

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(16)
            .IsRequired();
    }
}