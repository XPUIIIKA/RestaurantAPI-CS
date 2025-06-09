namespace Restaurant.Application.DTO.DishDTO;

public record PublicDishDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
}