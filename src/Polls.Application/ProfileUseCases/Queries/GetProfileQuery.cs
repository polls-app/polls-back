using MediatR;
using Polls.Application.ProfileUseCases.Models;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.ProfileUseCases.Queries;

public sealed record GetProfileQuery(UserId UserId) : IRequest<ProfileDto>;

public sealed class GetProfileQueryHandler(
    IProfileRepository profileRepository
) : IRequestHandler<GetProfileQuery, ProfileDto>
{
    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileByUserId(request.UserId);

        if (profile is null)
            throw new ApplicationException("Profile not found");

        return new ProfileDto(
            profile.Username.Value,
            profile.Fullname.Firstname.Value,
            profile.Fullname.Lastname.Value,
            profile.Description,
            profile.Avatar.Path,
            profile.FollowerCount,
            profile.FollowingCount,
            profile.PostCount,
            profile.ContributionCount);
    }
}