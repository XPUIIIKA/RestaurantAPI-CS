using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.DTO.DishDTO;

namespace Restaurant.Application.Interfaces;

public interface IAnalyticsService
{
    
    // Managers can do this:
    Task<ErrorOr<decimal>> GetTotalRevenueAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> GetTotalChecksCountAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    // Directors can do this:
    Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetTopSellingDishesAsync(CancellationToken cancellationToken);
}