using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class ProductImage
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;

    public string? Alt { get; set; }

    public string? Description { get; set; }

    public int ProductId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual Product Product { get; set; } = null!;
}
