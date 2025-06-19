using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Configurations;

public class DishInCheckConfig : IEntityTypeConfiguration<DishInCheck>
{
    public void Configure(EntityTypeBuilder<DishInCheck> builder)
    {
        builder.HasKey(d => new {d.CheckId, d.DishId});
        
        builder.HasOne(d => d.Check)
            .WithMany(d => d.Dishes)
            .HasForeignKey(d => d.CheckId);

        builder.HasOne(d => d.Dish)
            .WithMany()
            .HasForeignKey(d => d.DishId);
        
        builder.Property(d => d.DishId)
            .IsRequired();
        
        builder.Property(d => d.CheckId)
            .IsRequired();
        
        builder.Property(d => d.Price)
            .HasColumnType("decimal(8,2)")
            .IsRequired();
        
        builder.Property(d => d.ProductionPrice)
            .HasColumnType("decimal(8,2)")
            .IsRequired();
    }
}