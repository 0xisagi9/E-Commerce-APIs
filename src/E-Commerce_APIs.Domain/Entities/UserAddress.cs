using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class UserAddress
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Location { get; set; }

    public string? Mobile { get; set; }

    public string? PostalCode { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
