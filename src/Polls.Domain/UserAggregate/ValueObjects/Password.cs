using System.Security.Cryptography;

namespace Polls.Domain.UserAggregate.ValueObjects;

public sealed class Password
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int HashIterations = 100000;

    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;

    public Password(string hashed)
    {
        Hashed = hashed;
    }

    public string Hashed { get; }

    public int Length => Hashed.Length;

    public bool Verify(string plainText)
    {
        var parts = Hashed.Split('-');

        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(plainText, salt, HashIterations, HashAlgorithmName, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }

    public static Password CreateHashed(string plainText)
    {
        var hash = Hash(plainText);
        return new Password(hash);
    }

    private static string Hash(string plainText)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(plainText, salt, HashIterations, HashAlgorithmName, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }
}