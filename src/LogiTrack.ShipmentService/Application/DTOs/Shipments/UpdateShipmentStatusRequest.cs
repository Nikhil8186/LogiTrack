using System.ComponentModel.DataAnnotations;
using LogiTrack.ShipmentService.Domain.Enums;

namespace LogiTrack.ShipmentService.Application.DTOs.Shipments;

public class UpdateShipmentStatusRequest
{
    [EnumDataType(typeof(ShipmentStatus))]
    public ShipmentStatus Status { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}