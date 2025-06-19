using Restaurant.Domain.Entities;

namespace Restaurant.Domain.IRepositories;

public interface IDishRepository
{
    Task<IEnumerable<Dish>> GetAllAsync (CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Dish>> GetAsyncByPart (string part, CancellationToken cancellationToken = default);

    Task<IEnumerable<Dish>> GetDishesByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    
    Task<Dish?> GetDishAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Dish?> UpdateDishAsync(Dish  dish);

    Task<Dish?> AddDishAsync(Dish dish);
    
    Task<Dish?> DeleteDishAsync(Guid id);
}