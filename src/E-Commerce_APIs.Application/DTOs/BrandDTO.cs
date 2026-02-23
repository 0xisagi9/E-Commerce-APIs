namespace E_Commerce_APIs.Application.DTOs;

public class BrandDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Slug { get; set; }
}
