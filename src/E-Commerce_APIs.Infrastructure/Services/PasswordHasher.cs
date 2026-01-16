using E_Commerce_APIs.Shared.Interfaces;


namespace E_Commerce_APIs.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordhash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordhash);
    }
}
