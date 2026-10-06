using LogiTrack.ShipmentService.Application.DTOs.Shipments;

namespace LogiTrack.ShipmentService.Application.Interfaces;

public interface IShipmentService
{
    Task<int> CreateShipmentAsync(
        CreateShipmentRequest request);

    Task<ShipmentResponse?> GetByIdAsync(
        int shipmentId);

    Task<ShipmentResponse?> GetByTrackingNumberAsync(
        string trackingNumber);

    Task<ShipmentResponse?> GetByOrderIdAsync(
        int orderId);

    Task<bool> UpdateStatusAsync(
        int shipmentId,
        UpdateShipmentStatusRequest request);
}