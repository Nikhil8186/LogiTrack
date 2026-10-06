namespace LogiTrack.ShipmentService.Application.DTOs.Shipments;

public class TrackingEventResponse
{
    public int TrackingEventId { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }
}