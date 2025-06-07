using MediatR;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.ProfileUseCases.Commands;

public sealed record UpdateProfileCommand(
    string? Username,
    string? Firstname,
    string? Lastname,
    string? Description,
    UserId UserId
) : IRequest;

public sealed class UpdateProfileCommandHandler(
    IProfileRepository profileRepository
) : IRequestHandler<UpdateProfileCommand>
{
    public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileByUserId(request.UserId);
        if (profile is null)
            throw new ApplicationException("Profile not found");

        var username = request.Username is null
            ? profile.Username
            : new Username(request.Username);

        var fullname = request.Firstname is null || request.Lastname is null
            ? profile.Fullname
            : new Fullname(new Name(request.Firstname), new Name(request.Lastname));

        if (await profileRepository.IsUsernameTaken(username))
            throw new ApplicationException("Username already taken");

        profile.Update(username, fullname, request.Description ?? profile.Description);
        await profileRepository.UpdateProfile(profile);
    }
}