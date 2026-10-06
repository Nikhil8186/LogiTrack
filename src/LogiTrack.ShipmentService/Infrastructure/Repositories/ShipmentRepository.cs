using LogiTrack.ShipmentService.Application.Interfaces;
using LogiTrack.ShipmentService.Domain.Entities;
using LogiTrack.ShipmentService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.ShipmentService.Infrastructure.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly ShipmentDbContext _dbContext;

    public ShipmentRepository(ShipmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Shipment?> GetByIdAsync(int shipmentId)
    {
        return await _dbContext.Shipments
            .Include(x => x.TrackingEvents)
            .FirstOrDefaultAsync(
                x => x.ShipmentId == shipmentId);
    }

    public async Task<Shipment?> GetByTrackingNumberAsync(
        string trackingNumber)
    {
        return await _dbContext.Shipments
            .Include(x => x.TrackingEvents)
            .FirstOrDefaultAsync(
                x => x.TrackingNumber == trackingNumber);
    }

    public async Task<Shipment?> GetByOrderIdAsync(
        int orderId)
    {
        return await _dbContext.Shipments
            .Include(x => x.TrackingEvents)
            .FirstOrDefaultAsync(
                x => x.OrderId == orderId);
    }

    public async Task AddAsync(Shipment shipment)
    {
        await _dbContext.Shipments.AddAsync(shipment);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}