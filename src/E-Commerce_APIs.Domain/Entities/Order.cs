using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderShippingAddress? OrderShippingAddress { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

}
