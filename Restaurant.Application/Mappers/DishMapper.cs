using Restaurant.Application.DTO;
using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Domain.Entities;
using Restaurant.Domain.ShortElems;

namespace Restaurant.Application.Mappers;

public static class DishMapper
{
    public static ManagerDishDto GetManagerDish(Dish dish) => new ManagerDishDto
    {
        Id = dish.Id,
        Name = dish.Name,
        Description = dish.Description,
        ProductionPrice = dish.ProductionPrice,
        Price = dish.Price
    };

    public static PublicDishDto GetPublicDish(Dish dish) => new PublicDishDto
    {
        Name = dish.Name,
        Description = dish.Description,
        Price = dish.Price
    };

    public static PublicDishInCheckDto GetDishInCheck(DishInCheck dish) => new PublicDishInCheckDto
    {
        Id = dish.Id,
        Price = dish.Price
    };
}