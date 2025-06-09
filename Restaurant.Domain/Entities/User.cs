using Restaurant.Domain.BaseElements;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities;

public class User : EntityWithUpdate
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Login { get; set; }
    public required string HashPassword { get; set; }
    public required UserRole Role { get; set; }
    
}