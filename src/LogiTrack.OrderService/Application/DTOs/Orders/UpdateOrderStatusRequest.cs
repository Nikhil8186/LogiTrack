using System.ComponentModel.DataAnnotations;
using LogiTrack.OrderService.Domain.Enums;

namespace LogiTrack.OrderService.Application.DTOs.Orders;

public class UpdateOrderStatusRequest
{
    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }
}