using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.FollowerAggregate.Repositories;

public interface IFollowerRepository
{
    Task<bool> ExistsAsync(UserId followerId, UserId followeeId);

    Task AddAsync(Follower follower);
}