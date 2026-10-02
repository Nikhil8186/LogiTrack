using LogiTrack.OrderService.Application.DTOs;
using LogiTrack.OrderService.Application.DTOs.Orders;

namespace LogiTrack.OrderService.Application.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderAsync(CreateOrderRequest request);

    Task<OrderResponse?> GetOrderByIdAsync(int orderId);

    Task<PagedResult<OrderResponse>> GetOrdersAsync(
        OrderQueryRequest request);
    Task<bool> UpdateOrderStatusAsync(
    int orderId,
    UpdateOrderStatusRequest request);
}