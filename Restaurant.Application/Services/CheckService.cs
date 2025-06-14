using ErrorOr;
using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories;
using Restaurant.Application.Mappers;
using Deleted = Restaurant.Application.InfoClass.Deleted;


namespace Restaurant.Application.Services;

public class CheckService(
    ICheckRepository checkRepository,
    IUnitOfWork unitOfWork) : ICheckService
{
    public async Task<ErrorOr<ManagerCheckDto>> GetCheckById(Guid id, CancellationToken cancellationToken)
    { 
        if (Guid.Empty == id)
            return Error.Validation("Check", "The checks id must not be empty.");
        
        var result = await checkRepository.GetCheckAsync( id, cancellationToken);
        
        if (result is null)
            return Error.Validation("Check", "Id not found.");
        
        return CheckMapper.GetManagerCheck(result);
    }

    public async Task<ErrorOr<PublicCheckDto>> CreateCheck(CreateCheckDto createCheck)
    {
        var result = await checkRepository.CreateCheckAsync(createCheck);
        
        if  (result is null)
            return  Error.Validation("Check", "Create check failed.");

        await unitOfWork.SaveChangesAsync();
        
        return CheckMapper.GetPublicCheck(result);
    }
    
    
    public async Task<ErrorOr<Deleted>> DeleteCheck(Guid id)
    {
        if (Guid.Empty == id)
            return Error.Validation("Check", "The checks id must not be empty.");
        
        var pastCheck =  await checkRepository.DeleteCheckAsync(id);
        
        await unitOfWork.SaveChangesAsync();
        
        if (pastCheck is null)
            return Error.Validation("Check", "Check not found");
        
        return new Deleted();
    }
}