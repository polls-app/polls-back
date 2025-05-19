namespace Polls.Domain.Verification;

public sealed class VerificationToken
{
    public string Token { get; }

    public DateTime ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(10);

    public VerificationToken(string token)
    {
        if (token.Length != 8 || !token.All(char.IsDigit))
            throw new ArgumentException("Token must be an 8-digit numeric string.");

        Token = token;
    }

    public bool IsVerify(VerificationToken token) => IsVerify(token.Token);

    public bool IsVerify(string token)
    {
        if (ExpiresAt < DateTime.UtcNow)
            throw new ApplicationException("Token has expired");

        return token == Token;
    }

    public void ChangeExpiresAt(uint minutes) => ExpiresAt = DateTime.UtcNow.AddMinutes(minutes);

    public static VerificationToken New()
    {
        var token = Generate8DigitToken();
        return new VerificationToken(token);
    }

    private static string Generate8DigitToken()
    {
        var random = new Random();
        var digits = new char[8].AsSpan();

        for (var i = 0; i < digits.Length; i++)
        {
            digits[i] = (char)('0' + random.Next(0, 10));
        }

        return new string(digits);
    }
}
