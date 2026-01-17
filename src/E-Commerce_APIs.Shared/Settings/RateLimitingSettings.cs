namespace E_Commerce_APIs.API.Configurations;

public class RateLimitingSettings
{
    public int PermitLimit { get; set; } = 100;
    public int WindowInSeconds { get; set; } = 60;
    public int QueueLimit { get; set; } = 2;
}
