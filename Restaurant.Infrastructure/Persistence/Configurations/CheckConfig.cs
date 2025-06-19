using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Configurations;

public class CheckConfig : IEntityTypeConfiguration<Check>
{
    public void Configure(EntityTypeBuilder<Check> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasOne(c => c.Waiter)
            .WithMany()
            .HasForeignKey(c => c.WaiterId);
    }
}