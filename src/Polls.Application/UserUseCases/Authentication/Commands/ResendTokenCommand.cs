using MediatR;
using Polls.Application.Abstractions.Senders;
using Polls.Domain.Errors;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record ResendTokenCommand(Email Email) : IRequest;

public sealed class ResendTokenCommandHandler(
    IUserRepository userRepository,
    IVerificationTokenSender sender,
    IVerificationTokenRepository tokenStore
) : IRequestHandler<ResendTokenCommand>
{
    public async Task Handle(ResendTokenCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsEmailTakenAsync(request.Email))
            throw new NotFoundException("User not found.");

        var token = VerificationToken.New();

        await tokenStore.Store(request.Email, token);
        await sender.SendAsync(token, request.Email);
    }
}