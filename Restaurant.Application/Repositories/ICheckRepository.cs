using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Repositories;

public interface ICheckRepository
{
    Task<Check?> GetCheckAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Check?> CreateCheckAsync(CreateCheckDto createCheck);
    
    Task<Check?> DeleteCheckAsync(Guid id);
}