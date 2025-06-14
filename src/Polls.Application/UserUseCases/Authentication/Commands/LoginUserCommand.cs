using MediatR;
using Polls.Application.Abstractions.Authentication;
using Polls.Application.UserUseCases.Models;
using Polls.Domain.Errors;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record LoginUserCommand(Email Email, string Password) : IRequest<UserDto>;

public sealed class LoginUserCommandHandler(
    IUserRepository userRepository,
    IProfileRepository profileRepository,
    IJwtTokenGenerator jwtTokenGenerator
) : IRequestHandler<LoginUserCommand, UserDto>
{
    public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null || !user.Password.Verify(request.Password))
            throw new BadRequestException("Invalid email or password.");

        var token = jwtTokenGenerator.GenerateToken(user);
        var username = await profileRepository.GetUsernameById(user.Id);

        return new UserDto(user.Id.Value, user.Email.Value, username.Value, token);
    }
}
