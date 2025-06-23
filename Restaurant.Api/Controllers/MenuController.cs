using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Extensions;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;

namespace Restaurant.Api.Controllers;
/// <summary>
/// This is a controller for working with menu(dishes).
/// </summary>
/// <param name="service">Service for working with menu(dishes)</param>
[ApiController]
[Route("api/menu")]
public class MenuController(IMenuService service) : ControllerBase
{
    /// <summary>
    /// Get all public dishes in menu.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List public dish dto</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<PublicDishDto>),  StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicDishes(CancellationToken cancellationToken)
    {
        return (await service.GetPublicDishesAsync(cancellationToken)).ToActionResult();
    }
    /// <summary>
    /// Get all public dishes by part of the name or description in menu.
    /// </summary>
    /// <param name="part">Part for search</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List public dish dto</returns>
    [HttpGet("by-part/{part}")]
    [ProducesResponseType(typeof(IEnumerable<PublicDishDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicDishesByPart(string part, CancellationToken cancellationToken)
    {
        return (await service.GetPublicDishesByPartAsync(part, cancellationToken)).ToActionResult();
    }
    /// <summary>
    /// Get all manager dishes in menu.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List manager dish dto</returns>
    [HttpGet("all/for-manager")]
    [ProducesResponseType(typeof(IEnumerable<ManagerDishDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDishesForManager(CancellationToken cancellationToken)
    {
        return (await service.GetDishesForManagerAsync(cancellationToken)).ToActionResult();
    }
    /// <summary>
    /// Get manager dish by id in menu.
    /// </summary>
    /// <param name="id">Dish id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Manager dish dto</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ManagerDishDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDish(Guid id, CancellationToken cancellationToken)
    {
        return (await service.GetDishAsync(id, cancellationToken)).ToActionResult();
    }
    /// <summary>
    /// Update dish in menu.
    /// </summary>
    /// <param name="dish">Update dish dto</param>
    /// <returns>Manager dish dto</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ManagerDishDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDish([FromBody] UpdateDishDto dish)
    {
        return (await service.UpdateDishAsync(dish)).ToActionResult();
        
    }
    /// <summary>
    /// Create new dish in menu.
    /// </summary>
    /// <param name="dish">Create dish dto</param>
    /// <returns>Manager dish dto</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ManagerDishDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddDish([FromBody]CreateDishDto dish)
    {
        return (await service.AddDishAsync(dish)).ToActionResult();
    }
    /// <summary>
    /// Delete dish from menu.
    /// </summary>
    /// <param name="id">Dish id</param>
    /// <returns>Manager dish dto</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ManagerDishDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDish(Guid id)
    {
        return (await service.DeleteDishAsync(id)).ToActionResult();
    }
}