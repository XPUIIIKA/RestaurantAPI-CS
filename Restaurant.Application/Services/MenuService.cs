using ErrorOr;
using Microsoft.Extensions.Logging;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Mappers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepositories;
using Deleted = Restaurant.Application.InfoClass.Deleted;

namespace Restaurant.Application.Services;

public class MenuService(
    IDishRepository dishRepository,
    IUnitOfWork unitOfWork,
    ILogger<CheckService> logger) : IMenuService
{
    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishes(CancellationToken cancellationToken)
    {
        var dishes = await dishRepository.GetAllAsync(cancellationToken);

        var result = dishes.Select(d => DishMapper.GetPublicDish(d)).ToList();
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesByPart(string part, CancellationToken cancellationToken = default)
    {
        if (part.Length > 32)
        {
            return Error.Validation("part", "Search term is too long (max size of 32 characters)");
        }
        
        if (string.IsNullOrEmpty(part))
            return await GetPublicDishes(cancellationToken);
        
        
        var dishes = await dishRepository.GetAsyncByPart(part,  cancellationToken);
        
        var result = dishes.Select(d => DishMapper.GetPublicDish(d)).ToList();
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetDishesForManager(CancellationToken cancellationToken = default)
    {
        var dishes = await dishRepository.GetAllAsync(cancellationToken);
        
        var result = dishes.Select(d => DishMapper.GetManagerDish(d)).ToList();
        
        return result;
    }

    public async Task<ErrorOr<ManagerDishDto>> GetDish(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) 
            return Error.Validation("id", "Search term is empty");
        
        var dish = await dishRepository.GetDishAsync(id, cancellationToken);

        if (dish is null)
            return Error.Validation("dish", "Dish not found");

        return DishMapper.GetManagerDish(dish);
    }

    public async Task<ErrorOr<ManagerDishDto>> UpdateDish(UpdateDishDto dish)
    {
        var pastDish =  await dishRepository.GetDishAsync(dish.Id);
        
        if (pastDish is null)
            return Error.Validation("dish", "Dish not found");
        
        if (dish.IsEquivalentTo(pastDish))
            return DishMapper.GetManagerDish(pastDish);

        var newDish = new Dish
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            ProductionPrice = dish.ProductionPrice,
            Price = dish.Price,
            CreatedAt = pastDish.CreatedAt,
            UpdatedAt = DateTime.Now,
        };
        
        var respons = await dishRepository.UpdateDishAsync(newDish);
        
        await unitOfWork.SaveChangesAsync();
        
        if (respons is null)
            return Error.Validation("dish", "Dish not found");

        var result = DishMapper.GetManagerDish(respons);
        
        return result;
    }

    public async Task<ErrorOr<ManagerDishDto>> AddDish(CreateDishDto publicUpdateDish)
    {
        var newDish = new Dish
        {
            Id = Guid.NewGuid(),
            Name = publicUpdateDish.Name,
            Description = publicUpdateDish.Description,
            ProductionPrice = publicUpdateDish.ProductionPrice,
            Price = publicUpdateDish.Price,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        
        var result = await dishRepository.AddDishAsync(newDish);

        await unitOfWork.SaveChangesAsync();
        
        if (result is null)
            return Error.Validation("dish", "Dish not found");
        
        var managerDish = DishMapper.GetManagerDish(result);
        
        return managerDish;
    }

    public async Task<ErrorOr<Deleted>> DeleteDish(Guid id)
    {
        if (Guid.Empty == id)
            return Error.Validation("Check", "The checks id must not be empty.");
        
        var pastDish =  await dishRepository.DeleteDishAsync(id);
        
        await unitOfWork.SaveChangesAsync();
        
        if (pastDish is null)
            return Error.Validation("dish", "Dish not found");

        return new Deleted();
    }
}