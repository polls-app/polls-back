using MediatR;
using Polls.Application.Abstractions.Senders;
using Polls.Domain.UserAggregate.Events;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Events;

public class RegisterUserEvent(
    IVerificationTokenSender sender,
    IVerificationTokenRepository tokenStore
) : INotificationHandler<UserRegistered>
{
    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var token = VerificationToken.New();

        await tokenStore.Store(notification.Email, token);
        await sender.SendAsync(token, notification.Email);
    }
}