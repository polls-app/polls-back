using MediatR;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record VerifyEmailCommand(string Token, Email Email) : IRequest;

public sealed class VerifyEmailCommandHandler(
    IUserRepository userRepository,
    IVerificationTokenRepository tokenStore
) : IRequestHandler<VerifyEmailCommand>
{
    public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await tokenStore.Get(request.Email);

        if (token is null)
            throw new ApplicationException("Token not found");

        if (!token.IsVerify(request.Token))
            throw new ApplicationException("Invalid token");

        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            throw new ApplicationException("User not found");

        user.Email.Verify();
        await userRepository.UpdateAsync(user);
    }
}