using System.ComponentModel.DataAnnotations;
using Restaurant.Domain.ShortElems;

namespace Restaurant.Application.DTO.CheckDTO;

public record CheckReqestDto
{
    [Required]
    public required Guid Waiter { get; init; }
    
    [Required]
    [MinLength(1, ErrorMessage = "List must have at least one item")]
    [MaxLength(30, ErrorMessage = "List must have no more than 30 items")]
    public required List<DishInCheck> Dishes { get; init; }
}