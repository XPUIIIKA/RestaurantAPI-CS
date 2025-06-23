using ErrorOr;
using Restaurant.Application.DTO.CheckDTO;

namespace Restaurant.Application.Interfaces;

public interface ICheckService
{
    // Managers can do this:
    Task<ErrorOr<ManagerCheckDto>> GetCheckByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<ManagerCheckDto>> CreateCheckAsync(CreateCheckDto createCheck);
    Task<ErrorOr<ManagerCheckDto>> DeleteCheckAsync(Guid id);
}