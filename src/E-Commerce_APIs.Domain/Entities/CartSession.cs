using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class CartSession
{
    public int Id { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
