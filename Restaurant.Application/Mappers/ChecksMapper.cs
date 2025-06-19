using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mappers;

public static class ChecksMapper
{
    public static IEnumerable<Guid> GetOnlyDishesByChecks(IEnumerable<Check> checks)
    {
        return checks.SelectMany(check => check.Dishes.Select(dish => dish.DishId));
    }
}