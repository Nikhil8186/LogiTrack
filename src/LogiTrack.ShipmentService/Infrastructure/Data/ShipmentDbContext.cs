using LogiTrack.ShipmentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.ShipmentService.Infrastructure.Data;

public class ShipmentDbContext : DbContext
{
    public ShipmentDbContext(
        DbContextOptions<ShipmentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Shipment> Shipments { get; set; }

    public DbSet<TrackingEvent> TrackingEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Shipment>()
            .HasIndex(x => x.TrackingNumber)
            .IsUnique();

        modelBuilder.Entity<Shipment>()
            .HasIndex(x => x.OrderId);

        modelBuilder.Entity<TrackingEvent>()
            .HasIndex(x => x.ShipmentId);

        modelBuilder.Entity<TrackingEvent>()
            .HasOne(x => x.Shipment)
            .WithMany(x => x.TrackingEvents)
            .HasForeignKey(x => x.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}