using LogiTrack.ShipmentService.Domain.Enums;

namespace LogiTrack.ShipmentService.Application.DTOs.Shipments;

public class ShipmentResponse
{
    public int ShipmentId { get; set; }

    public int OrderId { get; set; }

    public string TrackingNumber { get; set; } = string.Empty;

    public ShipmentStatus Status { get; set; }

    public string OriginAddress { get; set; } = string.Empty;

    public string OriginCity { get; set; } = string.Empty;

    public string OriginState { get; set; } = string.Empty;

    public string DestinationAddress { get; set; } = string.Empty;

    public string DestinationCity { get; set; } = string.Empty;

    public string DestinationState { get; set; } = string.Empty;

    public DateTime? DispatchedDate { get; set; }

    public DateTime? DeliveredDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<TrackingEventResponse> TrackingEvents { get; set; }
        = new();
}