using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class OrderShippingAddress
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Location { get; set; }

    public string Mobile { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
