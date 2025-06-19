using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepositories;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Repositories;

public class DishRepository(RestaurantDbContext context) : IDishRepository
{
    public async Task<IEnumerable<Dish>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Dishes.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Dish>> GetAsyncByPart(string part, CancellationToken cancellationToken = default)
    {
        return await context.Dishes
            .Where(d => EF.Functions.Like(d.Name, $"%{part}%") ||
                             EF.Functions.Like(d.Description, $"%{part}%"))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Dish>> GetDishesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await context.Dishes
            .Where(d => ids.Contains(d.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Dish?> GetDishAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Dishes.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Dish?> UpdateDishAsync(Dish dish)
    {
        var updatedDish = await context.Dishes.FirstOrDefaultAsync(d => d.Id == dish.Id);

        if (updatedDish is null)
            return null;
        
        updatedDish.Name = dish.Name;
        updatedDish.Description = dish.Description;
        updatedDish.Price = dish.Price;
        updatedDish.ProductionPrice = dish.ProductionPrice;
        updatedDish.UpdatedAt = DateTime.Now;
        
        return updatedDish;
    }

    public async Task<Dish?> AddDishAsync(Dish dish)
    {
        return (await context.Dishes.AddAsync(dish)).Entity;
    }

    public async Task<Dish?> DeleteDishAsync(Guid id)
    {
        var dish = await context.Dishes.FirstOrDefaultAsync(d => d.Id == id);

        if (dish is null)
            return null;
        
        var result = context.Dishes.Remove(dish);
        
        return result.Entity;
    }
}