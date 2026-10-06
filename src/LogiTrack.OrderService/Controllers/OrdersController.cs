using LogiTrack.OrderService.Application.DTOs.Orders;
using LogiTrack.OrderService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request)
    {
        var orderId = await _orderService.CreateOrderAsync(request);

        return CreatedAtAction(
            nameof(CreateOrder),
            new { id = orderId },
            new
            {
                OrderId = orderId,
                Message = "Order created successfully."
            });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound(new
            {
                Message = "Order not found."
            });
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(
       [FromQuery] OrderQueryRequest request)
    {
        var result = await _orderService.GetOrdersAsync(request);

        return Ok(result);
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(
    int id,
    [FromBody] UpdateOrderStatusRequest request)
    {
        var updated = await _orderService
            .UpdateOrderStatusAsync(id, request);

        if (!updated)
        {
            return NotFound(new
            {
                Message = "Order not found."
            });
        }

        return Ok(new
        {
            OrderId = id,
            Status = request.Status,
            Message = "Order status updated successfully."
        });

    }

    [HttpPost("{id:int}/shipment")]
    public async Task<IActionResult> CreateShipment(int id)
    {
        var shipment =
            await _orderService.CreateShipmentForOrderAsync(id);

        if (shipment == null)
        {
            return NotFound(new
            {
                Message = "Order not found."
            });
        }

        return Ok(shipment);
    }
}