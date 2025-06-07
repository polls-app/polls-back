namespace Polls.Application.ProfileUseCases.Models;

public record ProfileDto(
    string Username,
    string Firstname,
    string Lastname,
    string Description,
    string AvatarPath,
    uint FollowerCount,
    uint FollowingCount,
    uint PostCount,
    uint ContributionCount
);