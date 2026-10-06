namespace LogiTrack.OrderService.Application.DTOs.Shipments;

public class CreateShipmentResponse
{
    public int ShipmentId { get; set; }

    public string TrackingNumber { get; set; } = string.Empty;
}