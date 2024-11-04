using Microsoft.AspNetCore.Mvc;
using Orders.Services.Orders;
using Orders.Services.Orders.Dto;
using System.Net;

namespace Orders.Api.Controllers.Orders;

/// <summary>
/// Product controller with authentication.
/// </summary>
[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>Returns all orders.</summary>
    /// <returns>Returns all orders.</returns>
    /// <response code ="200">Returns data for order page.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Orders</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _orderService.GetAsync();
        return Ok(result);
    }

    /// <summary>Returns all orders.</summary>
    /// <returns>Returns all orders.</returns>
    /// <response code ="200">Returns data for order page.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Orders</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var result = await _orderService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Create order.</summary>
    /// <returns>Status code.</returns>
    /// <param name="OrderCreateRequestDto">Order information.</param>
    /// <response code ="200">Returns Success.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Order</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost()]
    public async Task<IActionResult> InsertAsync(OrderCreateRequestDto orderCreateRequestDto)
    {
        var result = await _orderService.CreateOrderAsync(orderCreateRequestDto);
        return Created("Created", result);
    }

    /// <summary>Cancel an existing order.</summary>
    /// <param name="orderCrancelRequestDto">Order ID.</param>
    /// <response code ="200">Returns OK.</response>
    /// <response code ="404">Returns Order was not found.</response>
    /// <response code ="500">Returns a server error.</response>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [HttpPut("cancel")]
    public async Task<IActionResult> CancelAsync(string id)
    {
        await _orderService.CancelAsync(id);
        return Ok();
    }

    /// <summary>Pay an existing order.</summary>
    /// <param name="id">Order ID.</param>
    /// <response code ="200">Returns OK.</response>
    /// <response code ="404">Returns Order was not found.</response>
    /// <response code ="500">Returns a server error.</response>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [HttpPut("pay")]
    public async Task<IActionResult> PayAsync(string id)
    {
        await _orderService.PayAsync(id);
        return Ok();
    }
}
