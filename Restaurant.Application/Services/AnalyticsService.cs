using ErrorOr;
using Microsoft.Extensions.Logging;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Mappers;
using Restaurant.Domain.IRepositories;

namespace Restaurant.Application.Services;

public class AnalyticsService(
    IDishRepository dishRepository,
    ICheckRepository checkRepository,
    ILogger<CheckService> logger) : IAnalyticsService
{
    public async Task<ErrorOr<decimal>> GetTotalRevenue(DateTime? from = null, DateTime? to = null)
    {
        logger.LogDebug("GetTotalRevenue function started");

        if (from > to)
        {
            logger.LogWarning("'From' date cannot be later than 'To' date");
            return Error.Validation("DateRange", "'From' date cannot be later than 'To' date.");
        }

        if (from > DateTime.UtcNow)
        {
            logger.LogWarning("'From' date cannot be in the future");
            return Error.Validation("DateRange", "'From' date cannot be in the future.");
        }

        if (to > DateTime.UtcNow)
        {
            logger.LogWarning("'To' date cannot be in the future");
            return Error.Validation("DateRange", "'To' date cannot be in the future.");
        }

        var result = await checkRepository.GetTotalRevenue(from, to);

        if (result == 0)
        {
            logger.LogCritical("Total value of checks not found");
            return Error.Conflict("Check","Total value of checks not found.");
        }

        logger.LogInformation("GetTotalRevenue function finished");
        return result;
    }

    public async Task<ErrorOr<int>> GetTotalChecksCount(DateTime? from = null, DateTime? to = null)
    {
        logger.LogDebug("GetTotalChecksCount function started");
        
        if (from > to)
        {
            logger.LogWarning("'From' date cannot be later than 'To' date");
            return Error.Validation("DateRange", "'From' date cannot be later than 'To' date");
        }

        if (from > DateTime.UtcNow)
        {
            logger.LogWarning("'From' date cannot be in the future");
            return Error.Validation("DateRange", "'From' date cannot be in the future");
        }

        if (to > DateTime.UtcNow)
        {
            logger.LogWarning("'To' date cannot be in the future");
            return Error.Validation("DateRange", "'To' date cannot be in the future");
        }
        
        var result = await checkRepository.GetTotalChecksCount(from, to);

        if (result == 0)
        {
            logger.LogCritical("Total count of checks not found");
            return Error.Conflict("Check","Total count of checks not found");
        }
        
        logger.LogInformation("GetTotalChecksCount function finished");
        return result;
    }

    public async Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetTopSellingDishes(
        CancellationToken cancellationToken = default)
    {
        const int topSize = 5;
        
        logger.LogDebug("GetTopSellingDishes function started with topSize param: {topSize}", topSize);
        
        var checks = (await checkRepository.GetAllChecksAsync(cancellationToken)).ToList();

        if (!checks.Any())
        {
            logger.LogWarning("Checks list empty");
            return Error.Conflict("Check", "checks is empty");
        }

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

        var topDishes = (await dishRepository.GetDishesByIdsAsync(topGuids, cancellationToken)).ToList();

        if (topDishes.Count == 0)
        {
            logger.LogCritical("Dishes list empty");
            return Error.Conflict("Dish", "Dish is empty");
        }
        
        logger.LogInformation("GetTopSellingDishes function finished");
        return topDishes.Select(d => DishMapper.GetManagerDish(d)).ToList();
    }
}