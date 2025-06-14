namespace Restaurant.Application.IRepositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}