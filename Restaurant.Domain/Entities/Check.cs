using Restaurant.Domain.BaseElements;

namespace Restaurant.Domain.Entities;

public class Check : BaseEntity
{
    public required Guid WaiterId { get; init; }
    
    public User? Waiter { get; init; }
    public required List<DishInCheck> Dishes { get; init; }
    public decimal TotalPrice => SumTotalPrice();
    
    private decimal SumTotalPrice()
    {
        return Dishes.Sum(dish => dish.Price);
    }
}