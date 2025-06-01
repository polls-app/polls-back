using MediatR;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.ProfileUseCases.Commands;

public sealed record UpdateProfileCommand(
    string Username,
    string Firstname,
    string Lastname,
    string Description,
    UserId UserId
) : IRequest;

public sealed class UpdateProfileCommandHandler(
    IProfileRepository profileRepository
) : IRequestHandler<UpdateProfileCommand>
{
    public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var username = new Username(request.Username);
        var fullname = new Fullname(new Name(request.Firstname), new Name(request.Lastname));

        var profile = await profileRepository.GetProfileByUserId(request.UserId);

        if (profile is null)
            throw new ApplicationException("Profile not found");

        if(await profileRepository.IsUsernameTaken(username))
            throw new ApplicationException("Username already taken");

        profile.Update(username, fullname, request.Description);
        await profileRepository.UpdateProfile(profile);
    }
}