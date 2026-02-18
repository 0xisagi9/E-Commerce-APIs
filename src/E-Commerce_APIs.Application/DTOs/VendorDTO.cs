using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.DTOs;

public class VendorDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? WebsiteUrl { get; set; }

    public double? AverageRate { get; set; }

    public string? Slug { get; set; }
}
