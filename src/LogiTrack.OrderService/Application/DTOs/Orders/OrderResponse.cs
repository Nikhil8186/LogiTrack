using LogiTrack.OrderService.Domain.Enums;

namespace LogiTrack.OrderService.Application.DTOs.Orders;

public class OrderResponse
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalAmount { get; set; }

    public CustomerResponse? Customer { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new();
}