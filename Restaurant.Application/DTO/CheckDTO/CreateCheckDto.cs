using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTO.CheckDTO;

public record CreateCheckDto
{
    [Required]
    public required Guid WaiterId { get; init; }
    
    [Required]
    [MinLength(1, ErrorMessage = "List must have at least one item")]
    [MaxLength(30, ErrorMessage = "List must have no more than 30 items")]
    public required List<CreateCheckDishInCheckDto> Dishes { get; init; }
    
}