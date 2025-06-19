namespace Restaurant.Domain.Entities;

public class DishInCheck
{
    public required Guid DishId { get; init; }
    public Dish? Dish { get; init; }
    public required Guid CheckId { get; init; }
    public Check? Check { get; init; }
    public required uint Quantity { get; init; }
    public required decimal Price { get; init; }
    public required decimal ProductionPrice { get; init; }
}