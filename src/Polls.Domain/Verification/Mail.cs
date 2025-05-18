namespace Polls.Domain.Verification;

public sealed class Mail(string subject, string body, bool isHtml = false)
{
    public string Subject { get; private set; } = subject;

    public string Body { get; private set; } = body;

    public bool IsHtml { get; private set; } = isHtml;

    public static Mail CreateVerificationMail(VerificationToken token)
    {
        const string subject = "Your verification code from Poll!";
        var body = $"Code: {token.Token}<br/>Expires through {(token.ExpiresAt - DateTime.UtcNow).Minutes} minutes";

        return new Mail(subject, body, true);
    }
}