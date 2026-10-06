using LogiTrack.ShipmentService.Domain.Enums;

namespace LogiTrack.ShipmentService.Domain.Entities;

public class Shipment
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

    public DateTime? ModifiedDate { get; set; }

    public ICollection<TrackingEvent> TrackingEvents { get; set; }
        = new List<TrackingEvent>();
}