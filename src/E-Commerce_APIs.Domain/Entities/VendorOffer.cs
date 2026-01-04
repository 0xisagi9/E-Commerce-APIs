using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class VendorOffer
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Guid VendorId { get; set; }

    public decimal Price { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
