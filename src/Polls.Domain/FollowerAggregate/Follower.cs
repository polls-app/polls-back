using Polls.Domain.Base;
using Polls.Domain.FollowerAggregate.Events;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.FollowerAggregate;

public class Follower : AggregateRoot
{
    private Follower(UserId followerId, UserId followeeId)
    {
        FollowerId = followerId;
        FolloweeId = followeeId;
        FollowedAt = DateTime.UtcNow;
    }

    public UserId FollowerId { get; private set; }

    public UserId FolloweeId { get; private set; }

    public DateTime FollowedAt { get; private set; }

    public static Follower Follow(UserId followerId, UserId followeeId)
    {
        if (followerId == followeeId)
            throw new InvalidOperationException("Users cannot follow themselves.");

        var follower = new Follower(followerId, followeeId);

        follower.AddEvent(new UserFollowed(follower.FollowerId, follower.FolloweeId));
        return follower;
    }
}