using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Repositories;

namespace Restaurant.Application.Services;

public class AnalyticsService(
    IDishRepository dishRepository,
    ICheckRepository checkRepository) : IAnalyticsService
{
    public Task<ErrorOr<decimal>> GetTotalRevenue(DateTime? from = null, DateTime? to = null)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<int>> GetTotalChecksCount(DateTime? from = null, DateTime? to = null)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetTopSellingDishes(Guid restaurantId)
    {
        throw new NotImplementedException();
    }
}