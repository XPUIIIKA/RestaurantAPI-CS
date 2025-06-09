using Restaurant.Domain.Entities;

namespace Restaurant.Application.Repositories;

public interface IDishRepository
{
    Task<IEnumerable<Dish>> GetAllAsync (CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Dish>> GetAsyncByPart (string part, CancellationToken cancellationToken = default);
}