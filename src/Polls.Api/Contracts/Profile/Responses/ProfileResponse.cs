namespace Polls.Api.Contracts.Profile.Responses;

public record ProfileResponse(
    string Username,
    string Firstname,
    string Lastname,
    string Description,
    string AvatarPath,
    uint ContributionCount
);