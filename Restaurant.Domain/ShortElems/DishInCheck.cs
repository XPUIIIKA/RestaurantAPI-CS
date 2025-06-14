using Restaurant.Domain.Entities;

namespace Restaurant.Domain.ShortElems;

public record class DishInCheck
{
    public required Guid Id { get; init; }
    public required decimal Price { get; init; }
    public required decimal ProductionPrice { get; init; }
}