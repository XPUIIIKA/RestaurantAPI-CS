using Restaurant.Domain.Entities;

namespace Restaurant.Domain.IRepositories;

public interface ICheckRepository
{
    Task<IEnumerable<Check>> GetAllChecksAsync(CancellationToken cancellationToken = default);
    Task<Check?> GetCheckAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Check?> CreateCheckAsync(Check createCheck);
    Task<Check?> DeleteCheckAsync(Guid id);
    Task<decimal> GetTotalRevenueAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    Task<int> GetTotalChecksCountAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
}