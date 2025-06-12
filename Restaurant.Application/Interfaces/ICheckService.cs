using ErrorOr;
using Restaurant.Application.DTO.CheckDTO;

namespace Restaurant.Application.Interfaces;

public interface ICheckService
{
    // Managers can do this:
    Task<ErrorOr<ManagerCheckDto>> GetCheckById(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<PublicCheckDto>> CreateCheck(CreateCheckDto createCheck);
    Task<ErrorOr<Deleted>> DeleteCheck(Guid id);
}