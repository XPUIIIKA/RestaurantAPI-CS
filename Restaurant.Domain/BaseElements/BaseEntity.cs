namespace Restaurant.Domain.BaseElements;

public abstract class BaseEntity
{
    public required Guid Id { get; set; }
    
    public required DateTime CreatedAt { get; set; }
}