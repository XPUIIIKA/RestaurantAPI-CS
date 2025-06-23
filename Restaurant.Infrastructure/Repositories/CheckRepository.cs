using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepositories;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Repositories;

public class CheckRepository(RestaurantDbContext context) : ICheckRepository
{
    public async Task<IEnumerable<Check>> GetAllChecksAsync(CancellationToken cancellationToken = default)
    {
        return await context.Checks
            .Include(c => c.Dishes)
            .ToListAsync(cancellationToken);
    }

    public async Task<Check?> GetCheckAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Checks
            .Include(c => c.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Check?> CreateCheckAsync(Check createCheck)
    {
        var check = await context.Checks.AddAsync(createCheck);
        
        return check.Entity;
    }

    public async Task<Check?> DeleteCheckAsync(Guid id)
    {
        var check = await context.Checks.FirstOrDefaultAsync(x => x.Id == id);

        if (check is null)
            return null;
        
        var result = context.Checks.Remove(check);
        
        return result.Entity;
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
    {
        var toDate = to ?? DateTime.UtcNow;
        
        var fromDate = from ?? DateTime.MinValue;
        
        var allChecks =  await context.Checks
            .Include(c => c.Dishes)
            .Where(c => c.CreatedAt >= fromDate && c.CreatedAt <= toDate)
            .ToListAsync(cancellationToken);

        return allChecks
            .SelectMany(c => c.Dishes)
            .Sum(d => d.Price - d.ProductionPrice);
    }

    public async Task<int> GetTotalChecksCountAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
    {
        var toDate = to ?? DateTime.UtcNow;
        
        var fromDate = from ?? DateTime.MinValue;
        
        return await context.Checks
            .Include(c => c.Dishes)
            .Where(c => c.CreatedAt >= fromDate && c.CreatedAt <= toDate)
            .CountAsync(cancellationToken);
    }
}