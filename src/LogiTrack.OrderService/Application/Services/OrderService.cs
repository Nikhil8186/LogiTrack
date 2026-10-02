using LogiTrack.OrderService.Application.DTOs;
using LogiTrack.OrderService.Application.DTOs.Orders;
using LogiTrack.OrderService.Application.Interfaces;
using LogiTrack.OrderService.Application.Validators;
using LogiTrack.OrderService.Domain.Entities;
using LogiTrack.OrderService.Domain.Enums;
using LogiTrack.OrderService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.OrderService.Application.Services;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _dbContext;

    private readonly IOrderRepository _orderRepository;

    public OrderService(
      OrderDbContext dbContext,
      IOrderRepository orderRepository)
    {
        _dbContext = dbContext;
        _orderRepository = orderRepository;
    }

    public async Task<int> CreateOrderAsync(CreateOrderRequest request)
    {
        var customer = await _orderRepository
    .GetCustomerByIdAsync(request.CustomerId);

        if (customer == null)
        {
            throw new ArgumentException("Customer not found.");
        }

        if (request.Items == null || request.Items.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one item.");
        }

        var order = new Order
        {
            CustomerId = request.CustomerId,
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            CreatedDate = DateTime.UtcNow
        };

        foreach (var item in request.Items)
        {
            var orderItem = new OrderItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.Quantity * item.UnitPrice
            };

            order.OrderItems.Add(orderItem);
        }

        order.TotalAmount = order.OrderItems.Sum(x => x.TotalPrice);

        await _orderRepository.AddOrderAsync(order);

        await _orderRepository.SaveChangesAsync();

        return order.OrderId;
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(int orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return null;
        }

        return new OrderResponse
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,

            Customer = new CustomerResponse
            {
                CustomerId = order.Customer.CustomerId,
                FirstName = order.Customer.FirstName,
                LastName = order.Customer.LastName,
                Email = order.Customer.Email
            },

            Items = order.OrderItems.Select(item => new OrderItemResponse
            {
                OrderItemId = item.OrderItemId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            }).ToList()
        };
    }

    public async Task<PagedResult<OrderResponse>> GetOrdersAsync(
     OrderQueryRequest request)
    {
        var query = _dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(o =>
                    o.OrderNumber.Contains(request.Search) ||
                    o.Customer.FirstName.Contains(request.Search) ||
                    o.Customer.LastName.Contains(request.Search) ||
                    o.OrderItems.Any(item =>
                        item.ProductName.Contains(request.Search)));
            }
        }

        if (request.Status.HasValue)
        {
            query = query.Where(o =>
                (int)o.Status == request.Status.Value);
        }

        if (request.MinAmount.HasValue)
        {
            query = query.Where(o =>
                o.TotalAmount >= request.MinAmount.Value);
        }

        if (request.MaxAmount.HasValue)
        {
            query = query.Where(o =>
                o.TotalAmount <= request.MaxAmount.Value);
        }

        query = query.OrderByDescending(o => o.OrderDate);

        var totalRecords = await query.CountAsync();

        var pageNumber = request.PageNumber < 1
            ? 1
            : request.PageNumber;

        var pageSize = request.PageSize < 1
            ? 20
            : request.PageSize;

        pageSize = Math.Min(pageSize, 100);

        var orders = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var items = orders.Select(order => new OrderResponse
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,

            Customer = new CustomerResponse
            {
                CustomerId = order.Customer.CustomerId,
                FirstName = order.Customer.FirstName,
                LastName = order.Customer.LastName,
                Email = order.Customer.Email
            },

            Items = order.OrderItems.Select(item =>
                new OrderItemResponse
                {
                    OrderItemId = item.OrderItemId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList()
        }).ToList();

        return new PagedResult<OrderResponse>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(
                totalRecords / (double)pageSize)
        };
    }

    public async Task<bool> UpdateOrderStatusAsync(
       int orderId,
       UpdateOrderStatusRequest request)
    {
        var order = await _orderRepository
            .GetOrderByIdAsync(orderId);

        if (order == null)
        {
            return false;
        }

        if (!OrderStatusValidator.IsValidTransition(
                order.Status,
                request.Status))
        {
            throw new InvalidOperationException(
                $"Order cannot move from {order.Status} to {request.Status}.");
        }

        order.Status = request.Status;

        await _orderRepository.SaveChangesAsync();

        return true;
    }
}