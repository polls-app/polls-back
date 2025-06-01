using Polls.Domain.FollowerAggregate.Repositories;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.FollowerAggregate.Services;

public class FollowUserService(IUserRepository userRepository, IFollowerRepository followerRepository)
{
    public async Task<Follower> Follow(UserId followerId, UserId followeeId)
    {
        if (!await userRepository.ExistsAsync(followerId))
            throw new InvalidOperationException("Follower does not exist.");

        if (!await userRepository.ExistsAsync(followeeId))
            throw new InvalidOperationException("Followee does not exist.");

        if (await followerRepository.ExistsAsync(followerId, followeeId))
            throw new InvalidOperationException("Already following.");

        return Follower.Follow(followerId, followeeId);
    }
}