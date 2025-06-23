using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Extensions;
using Restaurant.Application.DTO.CheckDTO;
using Restaurant.Application.Interfaces;

namespace Restaurant.Api.Controllers;

/// <summary>
/// This is a controller for working with checks.
/// </summary>
/// <param name="service">Service for working with checks</param>
[ApiController]
[Route("api/check")]
public class CheckController(ICheckService service) : ControllerBase
{
    /// <summary>
    /// Get check by id.
    /// </summary>
    /// <param name="id">Checks id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Manager check dto</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ManagerCheckDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCheckById(Guid id , CancellationToken cancellationToken)
    {
        return (await service.GetCheckByIdAsync(id, cancellationToken)).ToActionResult();
    }
    /// <summary>
    /// Create new check.
    /// </summary>
    /// <param name="checkDto">Create check dto</param>
    /// <returns>Public check dto</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ManagerCheckDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCheck([FromBody] CreateCheckDto checkDto)
    {
        return (await service.CreateCheckAsync(checkDto)).ToActionResult();
    }
    /// <summary>
    /// Delete check by id.
    /// </summary>
    /// <param name="id">Checks id</param>
    /// <returns>Manager check dto</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ManagerCheckDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCheck(Guid id)
    {
        return (await service.DeleteCheckAsync(id)).ToActionResult();
    }
}