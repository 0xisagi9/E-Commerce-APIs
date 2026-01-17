using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;
public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
