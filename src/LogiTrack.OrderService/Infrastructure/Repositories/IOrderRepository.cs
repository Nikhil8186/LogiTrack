using LogiTrack.OrderService.Domain.Entities;

namespace LogiTrack.OrderService.Application.Interfaces;

public interface IOrderRepository
{
    Task<Customer?> GetCustomerByIdAsync(int customerId);

    Task<Order?> GetOrderByIdAsync(int orderId);

    Task AddOrderAsync(Order order);

    Task SaveChangesAsync();
}