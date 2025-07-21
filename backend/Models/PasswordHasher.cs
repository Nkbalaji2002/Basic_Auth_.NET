using System.Security.Cryptography;
using System.Text;

namespace backend.Models;

public class PasswordHasher
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        passwordHash = hmac.ComputeHash(passwordBytes);
    }

    public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] computedHash = hmac.ComputeHash(passwordBytes);
        bool hashesMatch = computedHash.SequenceEqual(storedHash);
        return hashesMatch;
    }
}