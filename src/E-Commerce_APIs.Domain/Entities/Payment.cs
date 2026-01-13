using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;
public partial class Payment
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public string Provider { get; set; } = null!;

    public int StatusId { get; set; }

    public int OrderId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
