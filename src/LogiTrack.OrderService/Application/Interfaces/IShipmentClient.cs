using LogiTrack.OrderService.Application.DTOs.Shipments;

namespace LogiTrack.OrderService.Application.Interfaces;

public interface IShipmentClient
{
    Task<CreateShipmentResponse?> CreateShipmentAsync(
        CreateShipmentRequest request);
}