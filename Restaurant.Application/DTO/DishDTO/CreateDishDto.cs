using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTO.DishDTO;

public class CreateDishDto
{
    [Required]
    [MaxLength(32, ErrorMessage = "Name must be between 4 and 32 characters")]
    [MinLength(4, ErrorMessage = "Name must be between 4 and 32 characters")]
    public required string Name { get; init; }
    
    [Required]
    [MaxLength(128, ErrorMessage = "Description must be between 10 and 128 characters")]
    [MinLength(10, ErrorMessage = "Description must be between 10 and 128 characters")]
    public required string Description { get; init; }
    
    [Required]
    [Range(0.00, 8000.00, ErrorMessage = "ProductionPrice must be between 00.00 and 8000.00")]
    public required decimal ProductionPrice  { get; init; }
    
    [Required]
    [Range(10.00, 10000.00, ErrorMessage = "Price must be between 10.00 and 10000.00")]
    public required decimal Price { get; init; }
}