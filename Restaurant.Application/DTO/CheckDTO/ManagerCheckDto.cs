namespace Restaurant.Application.DTO.CheckDTO;

public class ManagerCheckDto
{
    public required Guid Id { get; set; }
    public required Guid Waiter { get; init; }
    public required IEnumerable<PublicDishInCheckDto> Dishes { get; init; }
    public decimal TotalPrice => SumTotalPrice();
    private decimal SumTotalPrice()
    {
        return Dishes.Sum(dish => dish.Price);
    }
}