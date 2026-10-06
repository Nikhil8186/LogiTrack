using LogiTrack.ShipmentService.Domain.Enums;

namespace LogiTrack.ShipmentService.Application.Validators;

public static class ShipmentStatusValidator
{
    public static bool IsValidTransition(
        ShipmentStatus currentStatus,
        ShipmentStatus newStatus)
    {
        return currentStatus switch
        {
            ShipmentStatus.Created =>
                newStatus == ShipmentStatus.Assigned ||
                newStatus == ShipmentStatus.Cancelled,

            ShipmentStatus.Assigned =>
                newStatus == ShipmentStatus.Dispatched ||
                newStatus == ShipmentStatus.Cancelled,

            ShipmentStatus.Dispatched =>
                newStatus == ShipmentStatus.InTransit ||
                newStatus == ShipmentStatus.Failed,

            ShipmentStatus.InTransit =>
                newStatus == ShipmentStatus.AtHub ||
                newStatus == ShipmentStatus.Failed,

            ShipmentStatus.AtHub =>
                newStatus == ShipmentStatus.OutForDelivery ||
                newStatus == ShipmentStatus.Failed,

            ShipmentStatus.OutForDelivery =>
                newStatus == ShipmentStatus.Delivered ||
                newStatus == ShipmentStatus.Failed,

            ShipmentStatus.Failed =>
                newStatus == ShipmentStatus.Assigned ||
                newStatus == ShipmentStatus.Cancelled,

            ShipmentStatus.Delivered =>
                false,

            ShipmentStatus.Cancelled =>
                false,

            _ => false
        };
    }
}