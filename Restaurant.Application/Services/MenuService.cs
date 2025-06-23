using ErrorOr;
using Microsoft.Extensions.Logging;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Mappers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepositories;

namespace Restaurant.Application.Services;

public class MenuService(
    IDishRepository dishRepository,
    IUnitOfWork unitOfWork,
    ILogger<CheckService> logger) : IMenuService
{
    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GetPublicDishes function started");
        
        var dishes = await dishRepository.GetAllAsync(cancellationToken);

        var result = dishes.Select(d => DishMapper.GetPublicDish(d)).ToList();
        
        logger.LogInformation("GetPublicDishes function finished {result}", result);
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesByPartAsync(string part, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetPublicDishesByPart function started");
        
        if (part.Length > 32)
        {
            logger.LogWarning("Search term is too long (max size of 32 characters)");
            return Error.Validation("part", "Search term is too long (max size of 32 characters)");
        }
        
        if (string.IsNullOrEmpty(part))
            return await GetPublicDishesAsync(cancellationToken);
        
        
        var dishes = await dishRepository.GetAsyncByPartAsync(part,  cancellationToken);
        
        var result = dishes.Select(d => DishMapper.GetPublicDish(d)).ToList();
        
        logger.LogInformation("GetPublicDishesByPart function finished {result}", result);
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetDishesForManagerAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GetDishesForManager function started");
        
        var dishes = await dishRepository.GetAllAsync(cancellationToken);
        
        var result = dishes.Select(d => DishMapper.GetManagerDish(d)).ToList();
        
        logger.LogInformation("GetDishesForManager function finished {result}", result);
        
        return result;
    }

    public async Task<ErrorOr<ManagerDishDto>> GetDishAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetDish function started");

        if (id == Guid.Empty)
        {
            logger.LogWarning("Search term is empty {id}", id);
            return Error.Validation("id", "Search term is empty");
        }
        
        var dish = await dishRepository.GetDishAsync(id, cancellationToken);

        if (dish is null)
        {
            logger.LogWarning("Dish not found");
            return Error.Validation("dish", "Dish not found");
        }
        
        logger.LogInformation("GetDish function finished {dish}", dish);
        
        return DishMapper.GetManagerDish(dish);
    }

    public async Task<ErrorOr<ManagerDishDto>> UpdateDishAsync(UpdateDishDto dish)
    {
        logger.LogInformation("UpdateDish function started");
        
        var pastDish =  await dishRepository.GetDishAsync(dish.Id);

        if (pastDish is null)
        {
            logger.LogWarning("Dish not found");
            return Error.Validation("dish", "Dish not found");
        }

        if (dish.IsEquivalentTo(pastDish))
        {
            logger.LogWarning("The data is the same as before");
            return DishMapper.GetManagerDish(pastDish);
        }

        var newDish = new Dish
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            ProductionPrice = dish.ProductionPrice,
            Price = dish.Price,
            CreatedAt = pastDish.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
        };
        
        var respons = await dishRepository.UpdateDishAsync(newDish);
        
        await unitOfWork.SaveChangesAsync();

        if (respons is null)
        {
            logger.LogError("Update failed");
            return Error.Conflict("dish", "Update failed");
        }

        var result = DishMapper.GetManagerDish(respons);
        
        logger.LogInformation("UpdateDish function finished {result}", result);
        
        return result;
    }

    public async Task<ErrorOr<ManagerDishDto>> AddDishAsync(CreateDishDto publicUpdateDish)
    {
        logger.LogInformation("AddDish function started");
        
        var newDish = new Dish
        {
            Id = Guid.NewGuid(),
            Name = publicUpdateDish.Name,
            Description = publicUpdateDish.Description,
            ProductionPrice = publicUpdateDish.ProductionPrice,
            Price = publicUpdateDish.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        var result = await dishRepository.AddDishAsync(newDish);

        if (result is null)
        {
            logger.LogError("Add dish failed");
            return Error.Validation("dish", "Add dish failed");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        var managerDish = DishMapper.GetManagerDish(result);
        
        logger.LogInformation("UpdateDish function finished {result}", result);
        
        return managerDish;
    }

    public async Task<ErrorOr<ManagerDishDto>> DeleteDishAsync(Guid id)
    {
        logger.LogInformation("DeleteDish function started");

        if (Guid.Empty == id)
        {
            logger.LogWarning("Search term is empty {id}", id);
            return Error.Validation("Check", "The checks id must not be empty.");
        }
        
        var pastDish =  await dishRepository.DeleteDishAsync(id);

        if (pastDish is null)
        {
            logger.LogError("Delete failed");
            return Error.Validation("dish", "Delete failed");
        }
        
        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("UpdateDish function finished {pastDish}", pastDish);
        
        return DishMapper.GetManagerDish(pastDish);
    }
}