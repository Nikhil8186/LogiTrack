using System.ComponentModel.DataAnnotations;

namespace LogiTrack.ShipmentService.Application.DTOs.Shipments;

public class CreateShipmentRequest
{
    [Range(1, int.MaxValue)]
    public int OrderId { get; set; }

    [Required]
    [MinLength(1)]
    public string OriginAddress { get; set; } = string.Empty;

    [Required]
    public string OriginCity { get; set; } = string.Empty;

    [Required]
    public string OriginState { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string DestinationAddress { get; set; } = string.Empty;

    [Required]
    public string DestinationCity { get; set; } = string.Empty;

    [Required]
    public string DestinationState { get; set; } = string.Empty;
}