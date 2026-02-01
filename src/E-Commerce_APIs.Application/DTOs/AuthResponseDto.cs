using E_Commerce_APIs.Application.DTOs;
namespace E_Commerce_APIs.Application.DTOs;

public class AuthResponseDto
{
    public string? AccessToken { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public UserDto? User { get; set; }
}
