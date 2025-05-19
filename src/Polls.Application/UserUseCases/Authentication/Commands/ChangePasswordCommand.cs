using MediatR;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record ChangePasswordCommand(
    Email Email,
    Password Password,
    VerificationToken Token
) : IRequest;

public sealed class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IVerificationTokenRepository tokenStore
) : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var token = await tokenStore.Get(request.Email);

        if (token is null)
            throw new ApplicationException("Token not found");

        if (!token.IsVerify(request.Token))
            throw new ApplicationException("Invalid token");

        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            throw new ApplicationException("User not found");

        user.ChangePassword(request.Password);
        await userRepository.UpdateAsync(user);
    }
}