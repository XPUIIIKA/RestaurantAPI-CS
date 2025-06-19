using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Configurations;

public class DishConfig : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(d => d.Description)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(d => d.Price)
            .HasColumnType("decimal(8,2)")
            .IsRequired();
        
        builder.Property(d => d.ProductionPrice)
            .HasColumnType("decimal(8,2)")
            .IsRequired();
    }
}