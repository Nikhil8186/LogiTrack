using LogiTrack.ShipmentService.Domain.Entities;

namespace LogiTrack.ShipmentService.Application.Interfaces;

public interface IShipmentRepository
{
    Task<Shipment?> GetByIdAsync(int shipmentId);

    Task<Shipment?> GetByTrackingNumberAsync(
        string trackingNumber);

    Task<Shipment?> GetByOrderIdAsync(
        int orderId);

    Task AddAsync(Shipment shipment);

    Task SaveChangesAsync();
}