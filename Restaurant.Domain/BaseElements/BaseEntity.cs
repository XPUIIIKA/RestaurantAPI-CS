namespace Restaurant.Domain.BaseElements;

public abstract class BaseEntity
{
    public required Guid Id { get; set; }
    
    public required DateTime CreatedAd { get; set; }
}