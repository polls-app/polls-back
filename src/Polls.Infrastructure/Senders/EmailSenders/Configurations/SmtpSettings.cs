namespace Polls.Infrastructure.Senders.EmailSenders.Configurations;

public class SmtpSettings
{
    public required string Host { get; init; }

    public int Port { get; init; }

    public bool UseSsl { get; init; }

    public required string FromAddress { get; init; }

    public required string FromName { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }
}
