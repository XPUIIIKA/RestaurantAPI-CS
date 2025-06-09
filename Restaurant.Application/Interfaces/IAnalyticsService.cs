using ErrorOr;
using Restaurant.Application.DTO;

namespace Restaurant.Application.Interfaces;

public interface IAnalyticsService
{
    
    // Managers can do this:
    Task<ErrorOr<decimal>> GetTotalRevenue(DateTime? from = null, DateTime? to = null);
    Task<ErrorOr<int>> GetTotalChecksCount(DateTime? from = null, DateTime? to = null);
    
    // Directors can do this
    Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetTopSellingDishes(Guid restaurantId);
}