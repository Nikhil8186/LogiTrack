namespace LogiTrack.OrderService.Application.DTOs.Shipments;

public class CreateShipmentRequest
{
    public int OrderId { get; set; }

    public string OriginAddress { get; set; } = string.Empty;

    public string OriginCity { get; set; } = string.Empty;

    public string OriginState { get; set; } = string.Empty;

    public string DestinationAddress { get; set; } = string.Empty;

    public string DestinationCity { get; set; } = string.Empty;

    public string DestinationState { get; set; } = string.Empty;
}