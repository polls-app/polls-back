using Polls.Domain.Base.Events;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.FollowerAggregate.Events;

public sealed record UserFollowed(UserId FollowerId, UserId FolloweeId) : DomainEvent;
