using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.DTO.DishDTO;

namespace Restaurant.Application.Interfaces;

public interface IMenuService
{
    record struct Deleted;
    
    //Guest can do this:
    Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishes(CancellationToken cancellationToken = default);
    
    Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesByPart(string part, CancellationToken cancellationToken = default);
    
    
    // Managers can do this:
    Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetDishesForManager(CancellationToken cancellationToken = default);
    Task<ErrorOr<ManagerDishDto>> GetDish(Guid id, CancellationToken cancellationToken = default);
    Task<ErrorOr<ManagerDishDto>> UpdateDish(UpdateDishDto dish);
    Task<ErrorOr<ManagerDishDto>> AddDish(CreateDishDto dish);
    Task<ErrorOr<Deleted>> DeleteDish(Guid id);
}