using ErrorOr;
using Restaurant.Application.DTO.DishDTO;

namespace Restaurant.Application.Interfaces;

public interface IMenuService
{
    //Guest can do this:
    Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesAsync(CancellationToken cancellationToken);
    Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesByPartAsync(string part, CancellationToken cancellationToken);
    // Managers can do this:
    Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetDishesForManagerAsync(CancellationToken cancellationToken);
    Task<ErrorOr<ManagerDishDto>> GetDishAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ErrorOr<ManagerDishDto>> UpdateDishAsync(UpdateDishDto dish);
    Task<ErrorOr<ManagerDishDto>> AddDishAsync(CreateDishDto dish);
    Task<ErrorOr<ManagerDishDto>> DeleteDishAsync(Guid id);
}