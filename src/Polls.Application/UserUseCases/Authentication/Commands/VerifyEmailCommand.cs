using MediatR;
using Polls.Application.Abstractions.Authentication;
using Polls.Application.UserUseCases.Models;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record VerifyEmailCommand(VerificationToken Token, Email Email) : IRequest<UserDto>;

public sealed class VerifyEmailCommandHandler(
    IUserRepository userRepository,
    IProfileRepository profileRepository,
    IVerificationTokenRepository tokenStore,
    IJwtTokenGenerator jwtTokenGenerator
) : IRequestHandler<VerifyEmailCommand, UserDto>
{
    public async Task<UserDto> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
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

        var jwtToken = jwtTokenGenerator.GenerateToken(user);
        var username = await profileRepository.GetUsernameById(user.Id);

        return new UserDto(user.Id.Value, user.Email.Value, username.Value, jwtToken);
    }
}