using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;

namespace Polls.Application.Abstractions.Senders;

public interface IVerificationTokenSender
{
    Task SendAsync(VerificationToken token, Email email);
}