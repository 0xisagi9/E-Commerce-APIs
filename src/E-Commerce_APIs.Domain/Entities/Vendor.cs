using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class Vendor
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? WebsiteUrl { get; set; }

    public double? AverageRate { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Slug { get; set; }

    public virtual ICollection<VendorOffer> VendorOffers { get; set; } = new List<VendorOffer>();
}
