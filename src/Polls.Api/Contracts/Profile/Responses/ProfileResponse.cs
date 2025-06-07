using Polls.Application.ProfileUseCases.Models;

namespace Polls.Api.Contracts.Profile.Responses;

public record ProfileResponse(
    string Username,
    string Firstname,
    string Lastname,
    string Description,
    string AvatarPath,
    uint FollowerCount,
    uint FollowingCount,
    uint PostCount,
    uint ContributionCount
)
{
    public static ProfileResponse From(ProfileDto dto)
        => new(
            dto.Username,
            dto.Firstname,
            dto.Lastname,
            dto.Description,
            dto.AvatarPath,
            dto.FollowerCount,
            dto.FollowingCount,
            dto.PostCount,
            dto.ContributionCount);
}