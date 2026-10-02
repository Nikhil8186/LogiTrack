using System.Collections.Generic;

namespace LogiTrack.OrderService.Domain.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    // Navigation property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}