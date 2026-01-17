using System;
using System.Collections.Generic;

namespace E_Commerce_APIs.Domain.Entities;
public partial class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool IsVerified { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedDate { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public string? Slug { get; set; }

    public virtual ICollection<CartSession> CartSessions { get; set; } = new List<CartSession>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
