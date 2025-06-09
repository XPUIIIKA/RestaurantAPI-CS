using Restaurant.Domain.BaseElements;

namespace Restaurant.Domain.Entities;

public class Dish : EntityWithUpdate
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal ProductionPrice { get; set; }
    public required decimal Price { get; set; }
}