using MediatR;
using Polls.Domain.Errors;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.ProfileUseCases.Commands;

public sealed record UpdateAvatarCommand(
    string Path,
    UserId UserId
) : IRequest;

public sealed class UpdateAvatarCommandHandler(
    IProfileRepository profileRepository
) : IRequestHandler<UpdateAvatarCommand>
{
    public async Task Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
    {
        var avatar = new Avatar(request.Path);
        var profile = await profileRepository.GetProfileByUserId(request.UserId);

        if (profile is null)
            throw new NotFoundException();

        profile.ChangeAvatar(avatar);
        await profileRepository.UpdateProfile(profile);
    }
}