using LogiTrack.ShipmentService.Application.DTOs.Shipments;
using LogiTrack.ShipmentService.Application.Interfaces;
using LogiTrack.ShipmentService.Application.Validators;
using LogiTrack.ShipmentService.Domain.Entities;
using LogiTrack.ShipmentService.Domain.Enums;

namespace LogiTrack.ShipmentService.Application.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(
        IShipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreateShipmentAsync(
        CreateShipmentRequest request)
    {
        var existingShipment =
            await _repository.GetByOrderIdAsync(request.OrderId);

        if (existingShipment != null)
        {
            throw new InvalidOperationException(
                "A shipment already exists for this order.");
        }

        var shipment = new Shipment
        {
            OrderId = request.OrderId,
            TrackingNumber =
                $"TRK-{Guid.NewGuid():N}".ToUpperInvariant(),

            Status = ShipmentStatus.Created,

            OriginAddress = request.OriginAddress,
            OriginCity = request.OriginCity,
            OriginState = request.OriginState,

            DestinationAddress = request.DestinationAddress,
            DestinationCity = request.DestinationCity,
            DestinationState = request.DestinationState,

            CreatedDate = DateTime.UtcNow
        };

        shipment.TrackingEvents.Add(
            new TrackingEvent
            {
                Status = ShipmentStatus.Created.ToString(),
                Location = request.OriginCity,
                Description = "Shipment created.",
                EventDate = DateTime.UtcNow
            });

        await _repository.AddAsync(shipment);
        await _repository.SaveChangesAsync();

        return shipment.ShipmentId;
    }

    public async Task<ShipmentResponse?> GetByIdAsync(
        int shipmentId)
    {
        var shipment =
            await _repository.GetByIdAsync(shipmentId);

        return shipment == null
            ? null
            : MapToResponse(shipment);
    }

    public async Task<ShipmentResponse?> GetByTrackingNumberAsync(
        string trackingNumber)
    {
        var shipment =
            await _repository.GetByTrackingNumberAsync(
                trackingNumber);

        return shipment == null
            ? null
            : MapToResponse(shipment);
    }

    public async Task<ShipmentResponse?> GetByOrderIdAsync(
        int orderId)
    {
        var shipment =
            await _repository.GetByOrderIdAsync(orderId);

        return shipment == null
            ? null
            : MapToResponse(shipment);
    }

    public async Task<bool> UpdateStatusAsync(
      int shipmentId,
      UpdateShipmentStatusRequest request)
    {
        var shipment =
            await _repository.GetByIdAsync(shipmentId);

        if (shipment == null)
            return false;

        if (!ShipmentStatusValidator.IsValidTransition(
                shipment.Status,
                request.Status))
        {
            throw new InvalidOperationException(
                $"Shipment cannot move from " +
                $"{shipment.Status} to {request.Status}.");
        }

        shipment.Status = request.Status;

        if (request.Status == ShipmentStatus.Dispatched)
        {
            shipment.DispatchedDate = DateTime.UtcNow;
        }

        if (request.Status == ShipmentStatus.Delivered)
        {
            shipment.DeliveredDate = DateTime.UtcNow;
        }

        shipment.ModifiedDate = DateTime.UtcNow;

        shipment.TrackingEvents.Add(
            new TrackingEvent
            {
                Status = request.Status.ToString(),
                Location = request.Location,
                Description = request.Description,
                EventDate = DateTime.UtcNow
            });

        await _repository.SaveChangesAsync();

        return true;
    }

    private static ShipmentResponse MapToResponse(
        Shipment shipment)
    {
        return new ShipmentResponse
        {
            ShipmentId = shipment.ShipmentId,
            OrderId = shipment.OrderId,
            TrackingNumber = shipment.TrackingNumber,
            Status = shipment.Status,

            OriginAddress = shipment.OriginAddress,
            OriginCity = shipment.OriginCity,
            OriginState = shipment.OriginState,

            DestinationAddress = shipment.DestinationAddress,
            DestinationCity = shipment.DestinationCity,
            DestinationState = shipment.DestinationState,

            DispatchedDate = shipment.DispatchedDate,
            DeliveredDate = shipment.DeliveredDate,
            CreatedDate = shipment.CreatedDate,

            TrackingEvents = shipment.TrackingEvents
                .OrderByDescending(x => x.EventDate)
                .Select(x => new TrackingEventResponse
                {
                    TrackingEventId = x.TrackingEventId,
                    Status = x.Status,
                    Location = x.Location,
                    Description = x.Description,
                    EventDate = x.EventDate
                })
                .ToList()
        };
    }
}