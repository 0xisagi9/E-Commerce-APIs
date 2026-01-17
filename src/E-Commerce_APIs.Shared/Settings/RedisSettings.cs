namespace E_Commerce_APIs.API.Configurations;

public class RedisSettings
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}
