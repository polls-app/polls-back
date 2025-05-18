using MediatR;
using Polls.Application.Abstractions.Senders;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record ResendTokenCommand(string Email) : IRequest;

public sealed class ResendTokenCommandHandler(
    IUserRepository userRepository,
    IVerificationTokenSender sender,
    IVerificationTokenRepository tokenStore
) : IRequestHandler<ResendTokenCommand>
{
    public async Task Handle(ResendTokenCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);

        if (!await userRepository.IsEmailTakenAsync(email))
            throw new ApplicationException("User not found");

        var token = VerificationToken.New();

        await tokenStore.Store(email, token);
        await sender.SendAsync(token, email);
    }
}