using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;

public partial class Inventory
{
    public int Id { get; set; }

    public int VendorOfferId { get; set; }

    public int Quantity { get; set; }

    public int ReservedQuantity { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual VendorOffer VendorOffer { get; set; } = null!;
}
