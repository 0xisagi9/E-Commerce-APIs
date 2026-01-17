using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;
public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
