using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;

namespace Polls.Application.Abstractions.Senders;

public interface IEmailSender
{
    Task SendAsync(Mail mail, Email email);
}