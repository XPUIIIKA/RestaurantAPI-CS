using ErrorOr;
using Restaurant.Application.DTO.CheckDTO;

namespace Restaurant.Application.Interfaces;

public interface ICheckService
{
    
    // Managers can do this:
    Task<ErrorOr<CheckResponsDto>> GetCheckById(Guid id);
    Task<ErrorOr<CheckResponsDto>> CreateCheck(CheckReqestDto check);
    Task<ErrorOr<Deleted>> DeleteCheck(Guid id);
}