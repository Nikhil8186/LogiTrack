using LogiTrack.ShipmentService.Application.DTOs.Shipments;
using LogiTrack.ShipmentService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.ShipmentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly IShipmentService _shipmentService;

    public ShipmentsController(
        IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShipment(
        [FromBody] CreateShipmentRequest request)
    {
        var shipmentId =
            await _shipmentService.CreateShipmentAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = shipmentId },
            new
            {
                ShipmentId = shipmentId,
                Message = "Shipment created successfully."
            });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var shipment =
            await _shipmentService.GetByIdAsync(id);

        if (shipment == null)
        {
            return NotFound(new
            {
                Message = "Shipment not found."
            });
        }

        return Ok(shipment);
    }

    [HttpGet("tracking/{trackingNumber}")]
    public async Task<IActionResult> GetByTrackingNumber(
        string trackingNumber)
    {
        var shipment =
            await _shipmentService
                .GetByTrackingNumberAsync(trackingNumber);

        if (shipment == null)
        {
            return NotFound(new
            {
                Message = "Shipment not found."
            });
        }

        return Ok(shipment);
    }

    [HttpGet("order/{orderId:int}")]
    public async Task<IActionResult> GetByOrderId(
        int orderId)
    {
        var shipment =
            await _shipmentService.GetByOrderIdAsync(orderId);

        if (shipment == null)
        {
            return NotFound(new
            {
                Message = "Shipment not found."
            });
        }

        return Ok(shipment);
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        [FromBody] UpdateShipmentStatusRequest request)
    {
        var updated =
            await _shipmentService.UpdateStatusAsync(
                id,
                request);

        if (!updated)
        {
            return NotFound(new
            {
                Message = "Shipment not found."
            });
        }

        return Ok(new
        {
            ShipmentId = id,
            Status = request.Status,
            Message = "Shipment status updated successfully."
        });
    }
}