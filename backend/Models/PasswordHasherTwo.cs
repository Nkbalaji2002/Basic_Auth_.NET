using System.Security.Cryptography;
using System.Text;

namespace backend.Models;

public class PasswordHasherTwo
{
    /* Hashes a plian text password into a base64-encoded SHA-256 gasg */
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    /* Verifies Whether the provided plain password matches the stored hashed password */
    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return hashedPassword == HashPassword(providedPassword);
    }
}