namespace LogiTrack.ShipmentService.Domain.Entities;

public class TrackingEvent
{
    public int TrackingEventId { get; set; }

    public int ShipmentId { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public Shipment Shipment { get; set; } = null!;
}