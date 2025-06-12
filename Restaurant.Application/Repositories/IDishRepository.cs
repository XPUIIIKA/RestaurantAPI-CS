using Restaurant.Application.DTO.DishDTO;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Repositories;

public interface IDishRepository
{
    Task<IEnumerable<Dish>?> GetAllAsync (CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Dish>?> GetAsyncByPart (string part, CancellationToken cancellationToken = default);
    
    Task<Dish?> GetDishAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Dish?> UpdateDishAsync(UpdateDishDto  dish);

    Task<Dish?> AddDishAsync(CreateDishDto dish);
    
    Task<Dish?> DeleteDishAsync(Guid id);
}