using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_APIs.Domain.Entities;
public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? SmallImage { get; set; }

    public int? BrandId { get; set; }

    public int? ReviewsCount { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public string? Feature { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<VendorOffer> VendorOffers { get; set; } = new List<VendorOffer>();
}
