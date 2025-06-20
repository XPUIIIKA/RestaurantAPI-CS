using ErrorOr;
using Microsoft.Extensions.Logging;
using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Mappers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepositories;
using Deleted = Restaurant.Application.InfoClass.Deleted;


namespace Restaurant.Application.Services;

public class CheckService(
    IDishRepository dishRepository,
    ICheckRepository checkRepository,
    IUnitOfWork unitOfWork,
    ILogger<CheckService> logger) : ICheckService
{
    public async Task<ErrorOr<ManagerCheckDto>> GetCheckById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCheckById function started");
        
        if (Guid.Empty == id)
        {
            logger.LogWarning("Id :{id} is empty", id);
            return Error.Validation("Check", "The checks id must not be empty.");
        }
        
        var result = await checkRepository.GetCheckAsync( id, cancellationToken);

        if (result is null)
        {
            logger.LogError("Check not found");
            return Error.Validation("Check", "Check not found.");
        }
        
        logger.LogInformation("GetCheckById function finished {result}", result);
        return CheckMapper.GetManagerCheck(result);
    }

    public async Task<ErrorOr<PublicCheckDto>> CreateCheck(CreateCheckDto createCheck)
    {
        logger.LogInformation("CreateCheck function started");
        
        var newCheckId = Guid.NewGuid();
        
        var dishes = new List<DishInCheck>();

        if (!createCheck.Dishes.Any())
        {
            logger.LogWarning("The checks dishes list must not be empty");
            return Error.Validation("Check", "The checks dishes list must not be empty.");
        }
        
        foreach (var dishDto in createCheck.Dishes)
        {
            var dishInManu = await dishRepository.GetDishAsync(dishDto.DishId);

            if (dishInManu == null)
            {
                logger.LogError("Dish not found");
                return Error.Validation("Dish", "Dish not found.");
            }
            
            var dish = new DishInCheck
            {
                DishId = dishDto.DishId,
                CheckId = newCheckId,
                Quantity = dishDto.Quantity,
                Price = dishDto.Price,
                ProductionPrice = dishInManu.ProductionPrice
            };
            dishes.Add(dish);
        }
        
        var check = new Check
        {
            Id = newCheckId,
            WaiterId = createCheck.WaiterId,
            Dishes = dishes,
            CreatedAt = DateTime.Now,
        };
        
        var result = await checkRepository.CreateCheckAsync(check);

        if (result is null)
        {
            logger.LogError("Create check failed");
            return  Error.Validation("Check", "Create check failed.");
        }

        await unitOfWork.SaveChangesAsync();
        
        logger.LogInformation("CreateCheck function finished {result}", result);
        return CheckMapper.GetPublicCheck(result);
    }
    
    public async Task<ErrorOr<Deleted>> DeleteCheck(Guid id)
    {
        logger.LogInformation("DeleteCheck function started");

        if (Guid.Empty == id)
        {
            logger.LogWarning("The checks id must not be empty");
            return Error.Validation("Check", "The checks id must not be empty.");
        }
        
        var pastCheck =  await checkRepository.DeleteCheckAsync(id);

        if (pastCheck is null)
        {
            logger.LogError("Delete check failed");
            return Error.Validation("Check", "Delete check failed");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        logger.LogInformation("DeleteCheck function finished {pastCheck}", pastCheck);
        return new Deleted();
    }
}