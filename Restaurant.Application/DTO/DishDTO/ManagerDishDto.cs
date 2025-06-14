namespace Restaurant.Application.DTO.DishDTO;

public record ManagerDishDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal ProductionPrice { get; init; }
    public required decimal Price { get; init; }
}