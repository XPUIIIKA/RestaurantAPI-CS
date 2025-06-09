using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTO.CheckDTO;

public record DishInCheckDto
{
    [Required]
    public required Guid Id { get; init; }
    
    [Required]
    [Range(10.00, 10000.00, ErrorMessage = "Price must be between 10.00 and 10000.00")]
    public required decimal Price { get; init; }
}