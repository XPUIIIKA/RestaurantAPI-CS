namespace Restaurant.Domain.IRepositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}