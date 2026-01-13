using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;
public partial class CartItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public int CartSessionId { get; set; }

    public int VendorOfferId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual CartSession CartSession { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual VendorOffer VendorOffer { get; set; } = null!;
}
