using Polls.Domain.UserAggregate.Enums;

namespace Polls.Infrastructure.UserImplementations.Repositories.Dtos;

internal sealed class UserMapping
{
    public Guid Id { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required UserRole Role { get; init; }

    public bool IsVerified { get; init; }

    public DateTime LastLoginAt { get; init; }

    public DateTime JoinedAt { get; init; }
}