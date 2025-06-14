using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mappers;

public static class CheckMapper
{
    public static ManagerCheckDto GetManagerCheck(Check check) => new ManagerCheckDto
    {
        Id = check.Id,
        Waiter = check.Waiter,
        Dishes = check.Dishes.Select(d => new PublicDishInCheckDto
        {
            Id = d.Id,
            Price = d.Price,
        }),
    };
    
    public static PublicCheckDto GetPublicCheck(Check check) => new PublicCheckDto
    {
        Id = check.Id,
        Waiter = check.Waiter,
        Dishes = check.Dishes.Select(d => new PublicDishInCheckDto
        {
            Id = d.Id,
            Price = d.Price,
        }),
    };
}