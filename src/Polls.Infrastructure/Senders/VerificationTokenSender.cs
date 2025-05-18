using Polls.Application.Abstractions.Senders;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;

namespace Polls.Infrastructure.Senders;

public class VerificationTokenSender(IEmailSender sender) : IVerificationTokenSender
{
    public async Task SendAsync(VerificationToken token, Email email)
    {
        var mail = Mail.CreateVerificationMail(token);
        await sender.SendAsync(mail, email);
    }
}