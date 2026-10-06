namespace LogiTrack.ShipmentService.Domain.Enums;

public enum ShipmentStatus
{
    Created = 1,
    Assigned = 2,
    Dispatched = 3,
    InTransit = 4,
    AtHub = 5,
    OutForDelivery = 6,
    Delivered = 7,
    Failed = 8,
    Cancelled = 9
}