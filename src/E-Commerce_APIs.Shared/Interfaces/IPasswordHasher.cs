namespace E_Commerce_APIs.Shared.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    bool VerifyPassword(string password, string passwordhash);
}
