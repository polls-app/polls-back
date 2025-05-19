using MediatR;
using Polls.Application.Abstractions.Senders;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record SendChangePasswordEmailCommand(
    Email Email
) : IRequest;

public sealed class SendChangePasswordEmailCommandHandler(
    IVerificationTokenRepository tokenStore,
    IEmailSender emailSender
) : IRequestHandler<SendChangePasswordEmailCommand>
{
    public async Task Handle(SendChangePasswordEmailCommand request, CancellationToken cancellationToken)
    {
        var token = VerificationToken.New();
        var mail = Mail.CreateChangePasswordMail(token);

        await tokenStore.Store(request.Email, token);
        await emailSender.SendAsync(mail, request.Email);
    }
}