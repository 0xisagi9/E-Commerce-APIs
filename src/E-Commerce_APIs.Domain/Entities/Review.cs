using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class Review
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int Rate { get; set; }

    public bool? Reported { get; set; }

    public int OrderItemId { get; set; }

    public int UserStatusId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual OrderItem OrderItem { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ReviewUserStatus UserStatus { get; set; } = null!;
}
