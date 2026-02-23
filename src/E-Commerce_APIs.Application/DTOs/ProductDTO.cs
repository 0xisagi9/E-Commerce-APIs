using Npgsql.Replication;
using System.Text.Json;

namespace E_Commerce_APIs.Application.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string SmallImage { get; set; } = string.Empty;
    
    // Features as JSON STRING (not JsonDocument)
    // Client receives: {"processor":"Intel i9","ram":"32GB"}
    public string? Features { get; set; }
    
    public List<string> Categories { get; set; } = new List<string>();
}
