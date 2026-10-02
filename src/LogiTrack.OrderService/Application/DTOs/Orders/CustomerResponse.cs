namespace LogiTrack.OrderService.Application.DTOs.Orders;

public class CustomerResponse
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}