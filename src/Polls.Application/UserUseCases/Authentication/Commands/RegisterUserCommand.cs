using MediatR;
using Polls.Application.Abstractions.Authentication;
using Polls.Application.UserUseCases.Models;
using Polls.Domain.Base.Events;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.Services;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.UserUseCases.Authentication.Commands;

public sealed record RegisterUserCommand(string Email, string Password) : IRequest<UserDto>;

public sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    RegisterUserService registerUserService,
    IDomainEventDispatcher domainEventDispatcher
) : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        var password = Password.CreateHashed(request.Password);

        if (await userRepository.IsEmailTakenAsync(email))
            throw new ApplicationException("Email is already taken");

        var (user, profile) = registerUserService.Register(email, password);
        await userRepository.AddAsync(user, profile);

        var token = jwtTokenGenerator.GenerateToken(user);
        await domainEventDispatcher.DispatchAsync(user.Events);

        return new UserDto(user.Id.Value, user.Email.Value, profile.Username.Value, token);
    }
}