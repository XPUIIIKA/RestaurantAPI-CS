using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTO.CheckDTO;

public class CreateCheckDishInCheckDto
{
    [Required]
    public required Guid DishId { get; init; }
    
    [Required]
    [Range(10.00, 10000.00, ErrorMessage = "Price must be between 10.00 and 10000.00")]
    public required decimal Price { get; init; }
    
    [Required]
    [Range(10.00, 10000.00, ErrorMessage = "Price must be between 10.00 and 10000.00")]
    public required decimal ProductionPrice { get; init; }
    
    [Required]
    [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100")]
    public required uint Quantity { get; init; }
}