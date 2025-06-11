using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Mappers;
using Restaurant.Application.Repositories;

namespace Restaurant.Application.Services;

public class MenuService(IDishRepository dishRepository) : IMenuService
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

        var newDish = await dishRepository.UpdateDishAsync(dish);
        
        if (newDish is null)
            return Error.Validation("dish", "Dish not found");

        var result = DishMapper.GetManagerDish(newDish);

        return result;
    }

    public async Task<ErrorOr<ManagerDishDto>> AddDish(CreateDishDto publicUpdateDish)
    {
        if (publicUpdateDish is null)
            return Error.Validation("dish", "Dish not found");
        
        var newDish = await dishRepository.AddDishAsync(publicUpdateDish);

        if (newDish is null)
            return Error.Validation("dish", "Dish not found");
        
        var result = DishMapper.GetManagerDish(newDish);
        
        return result;
    }

    public async Task<ErrorOr<IMenuService.Deleted>> DeleteDish(Guid id)
    {
        var pastDish =  await dishRepository.DeleteDishAsync(id);
        
        if (pastDish is null)
            return Error.Validation("dish", "Dish not found");

        return new IMenuService.Deleted();
    }
}