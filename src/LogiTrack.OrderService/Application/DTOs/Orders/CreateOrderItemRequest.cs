using System.ComponentModel.DataAnnotations;

namespace LogiTrack.OrderService.Application.DTOs.Orders;

public class CreateOrderItemRequest
{
    [Required]
    [MinLength(1)]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal UnitPrice { get; set; }
}