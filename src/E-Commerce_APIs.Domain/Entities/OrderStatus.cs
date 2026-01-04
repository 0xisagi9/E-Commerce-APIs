using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
