using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Extensions;
using Restaurant.Application.DTO.DishDTO;
using Restaurant.Application.Interfaces;

namespace Restaurant.Api.Controllers;

/// <summary>
/// This is a controller for working with analytic.
/// </summary>
/// <param name="service">Service for working with analytic</param>
[ApiController]
[Route("api/analytic")]
public class AnalyticController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Get the total income from and to.
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <param name="from">From what date</param>
    /// <param name="to">To what date</param>
    /// <returns>Total revenue</returns>
    [HttpGet("revenue")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTotalRevenue(
        CancellationToken cancellationToken,
        [FromQuery]DateTime? from = null,
        [FromQuery]DateTime? to = null)
    {
        return (await service.GetTotalRevenueAsync(from, to, cancellationToken)).ToActionResult();
    }
    /// <summary>
    /// Get the total checks count from and to.
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <param name="from">From what date</param>
    /// <param name="to">To what date</param>
    /// <returns>Total checks count</returns>
    [HttpGet("checks-count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTotalChecksCount(
        CancellationToken cancellationToken,
        [FromQuery]DateTime? from = null,
        [FromQuery]DateTime? to = null)
    {
        return (await service.GetTotalChecksCountAsync(from, to, cancellationToken)).ToActionResult();
    }

    /// <summary>
    /// Get top 5 selling dishes.
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Top-selling dishes</returns>
    [HttpGet("top-dishes")]
    [ProducesResponseType(typeof(IEnumerable<ManagerDishDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopSellingDishes(CancellationToken cancellationToken)
    {
        return (await service.GetTopSellingDishesAsync(cancellationToken)).ToActionResult();
    }
}