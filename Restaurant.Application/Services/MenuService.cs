using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories;
using Restaurant.Application.Mappers;
using Deleted = Restaurant.Application.InfoClass.Deleted;

namespace Restaurant.Application.Services;

public class MenuService(
    IDishRepository dishRepository,
    IUnitOfWork unitOfWork) : IMenuService
{
    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishes(CancellationToken cancellationToken)
    {
        var dishes = await dishRepository.GetAllAsync(cancellationToken);
        
        if (dishes is null)
            return new List<PublicDishDto>(); 

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

        if (dishes is null)
            return new List<PublicDishDto>(); 
        
        var result = dishes.Select(d => DishMapper.GetPublicDish(d)).ToList();
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetDishesForManager(CancellationToken cancellationToken = default)
    {
        var dishes = await dishRepository.GetAllAsync(cancellationToken);

        if (dishes is null)
            return new List<ManagerDishDto>(); 
        
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
        
        await unitOfWork.SaveChangesAsync();
        
        if (newDish is null)
            return Error.Validation("dish", "Dish not found");

        var result = DishMapper.GetManagerDish(newDish);
        
        return result;
    }

    public async Task<ErrorOr<ManagerDishDto>> AddDish(CreateDishDto publicUpdateDish)
    {
        var newDish = await dishRepository.AddDishAsync(publicUpdateDish);

        await unitOfWork.SaveChangesAsync();
        
        if (newDish is null)
            return Error.Validation("dish", "Dish not found");
        
        var result = DishMapper.GetManagerDish(newDish);
        
        return result;
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