using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mappers;

public static class CheckMapper
{
    public static ManagerCheckDto GetManagerCheck(Check check) => new ManagerCheckDto
    {
        Id = check.Id,
        Waiter = check.WaiterId,
        Dishes = check.Dishes.Select(d => DishMapper.GetManagerDishInCheckDto(d)),
    };
    
    public static PublicCheckDto GetPublicCheck(Check check) => new PublicCheckDto
    {
        Id = check.Id,
        Waiter = check.WaiterId,
        Dishes = check.Dishes.Select(d => DishMapper.GetPublicDishInCheckDto(d)),
    };
}