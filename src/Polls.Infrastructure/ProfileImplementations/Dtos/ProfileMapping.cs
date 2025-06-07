namespace Polls.Infrastructure.ProfileImplementations.Dtos;

internal class ProfileMapping
{
    public Guid Id { get; init; }

    public required string Username { get; init; }

    public required string Firstname { get; init; }

    public required string Lastname { get; init; }

    public required string Description { get; init; }

    public required string AvatarPath { get; init; }

    public uint FollowerCount { get; init; }

    public uint FollowingCount { get; init; }

    public uint PostCount { get; init; }

    public uint ContributionCount { get; init; }

    public Guid UserId { get; init; }
}