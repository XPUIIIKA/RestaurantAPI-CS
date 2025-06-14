using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories;
using Restaurant.Application.Mappers;

namespace Restaurant.Application.Services;

public class AnalyticsService(
    IDishRepository dishRepository,
    ICheckRepository checkRepository) : IAnalyticsService
{
    public async Task<ErrorOr<decimal>> GetTotalRevenue(DateTime? from = null, DateTime? to = null)
    {
        if (from > to)
            return Error.Validation("DateRange", "'From' date cannot be later than 'To' date.");

        if (from > DateTime.UtcNow)
            return Error.Validation("DateRange", "'From' date cannot be in the future.");

        if (to > DateTime.UtcNow)
            return Error.Validation("DateRange", "'To' date cannot be in the future.");

        var result = await checkRepository.GetTotalRevenue(from, to);
        
        if (result == 0)
            return Error.Conflict("Check","Check total revenue not found.");

        return result;
    }

    public async Task<ErrorOr<int>> GetTotalChecksCount(DateTime? from = null, DateTime? to = null)
    {
        if (from > to)
            return Error.Validation("DateRange", "'From' date cannot be later than 'To' date.");

        if (from > DateTime.UtcNow)
            return Error.Validation("DateRange", "'From' date cannot be in the future.");

        if (to > DateTime.UtcNow)
            return Error.Validation("DateRange", "'To' date cannot be in the future.");
        
        
        var result = await checkRepository.GetTotalChecksCount(from, to);
        
        if (result == 0)
            return Error.Conflict("Check","Check total revenue not found.");

        return result;
    }

    public async Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetTopSellingDishes(
        CancellationToken cancellationToken = default)
    {
        const int topSize = 5;
        
        var checks = await checkRepository.GetAllChecksAsync(cancellationToken);
        
        if (checks is null)
            return Error.Conflict("Check", "checks is empty");

        var dishes = ChecksMapper.GetOnlyDishesByChecks(checks);

        var dishAndCount = dishes
            .GroupBy(guid => guid)
            .ToDictionary(
                group => group.Key,
                group => group.Count()
            );

        var topGuids = dishAndCount
            .OrderByDescending(d => d.Value)
            .Take(topSize)
            .Select(d => d.Key);

        var topDishes = await dishRepository.GetDishesByIdsAsync(topGuids, cancellationToken);
        
        if  (topDishes is null)
            return Error.Conflict("Dish", "Dish is empty");
        
        return topDishes.Select(d => DishMapper.GetManagerDish(d)).ToList();
    }
}