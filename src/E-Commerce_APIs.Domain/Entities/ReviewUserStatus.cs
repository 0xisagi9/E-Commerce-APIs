using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class ReviewUserStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
