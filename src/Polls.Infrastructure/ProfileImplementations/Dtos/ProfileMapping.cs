namespace Polls.Infrastructure.ProfileImplementations.Dtos;

public class ProfileMapping
{
    public Guid Id { get; init; }

    public required string Username { get; init; }

    public required string Firstname { get; init; }

    public required string Lastname { get; init; }

    public required string Description { get; init; }

    public required string AvatarPath { get; init; }

    public required string ContributionCount { get; init; }

    public Guid UserId { get; init; }
}