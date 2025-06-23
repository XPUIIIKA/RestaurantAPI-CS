using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Api.Extensions;

public static class ErrorOrExtensions
{
    public static IActionResult ToActionResult<T>(this ErrorOr<T> errorOr)
    {
        if (!errorOr.IsError)
            return new OkObjectResult(errorOr.Value);
        
        var firstError = errorOr.FirstError;

        return firstError.Type switch
        {
            ErrorType.NotFound  => new OkObjectResult(firstError),
            ErrorType.Conflict  => new ConflictObjectResult(firstError),
            
            _ => new BadRequestObjectResult(firstError)
        };
    }
}