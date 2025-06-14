namespace Restaurant.Domain.BaseElements;

public abstract class EntityWithUpdate :  BaseEntity
{
    public required DateTime UpdatedAt { get; set; }
}