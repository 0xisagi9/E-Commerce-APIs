using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class PaymentStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
