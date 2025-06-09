using ErrorOr;
using Restaurant.Application.DTO;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Repositories;

namespace Restaurant.Application.Services;

public class MenuService : IMenuService
{
    private readonly IDishRepository _dishRepository;

    public MenuService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }
    
    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishes(CancellationToken cancellationToken)
    {
        var dishes = await _dishRepository.GetAllAsync(cancellationToken);

        var result = dishes.Select(d => new PublicDishDto
        {
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
        }).ToList();
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<PublicDishDto>>> GetPublicDishesByPart(string part, CancellationToken cancellationToken = default)
    {
        if (part.Length > 32)
        {
            return Error.Validation("part", "Search term is too long (max size of 32 characters)");
        }
        
        if (string.IsNullOrEmpty(part))
        {
            return await GetPublicDishes(cancellationToken);
        }
        
        var dishes = await _dishRepository.GetAsyncByPart(part,  cancellationToken);

        var result = dishes.Select(d => new PublicDishDto
        {
            Name = d.Name,
            Description = d.Description,
            Price = d.Price
        }).ToList();
        
        return result;
    }

    public async Task<ErrorOr<IEnumerable<ManagerDishDto>>> GetDishesForManager(CancellationToken cancellationToken = default)
    {
        var dishes = await _dishRepository.GetAllAsync(cancellationToken);

        var result = dishes.Select(d => new ManagerDishDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            ProductionPrice = d.ProductionPrice,
            Price = d.Price
        }).ToList();
        
        return result;
    }

    public Task<ErrorOr<ManagerDishDto>> GetDish(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<ManagerDishDto>> UpdateDish(UpdateDishDto publicUpdateDish)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<ManagerDishDto>> AddDish(CreateDishDto publicUpdateDish)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<IMenuService.Deleted>> DeleteDish(Guid id)
    {
        throw new NotImplementedException();
    }
}