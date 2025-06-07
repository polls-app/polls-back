using Polls.Domain.FollowerAggregate;
using Polls.Domain.FollowerAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Infrastructure.Common.Persistence;

namespace Polls.Infrastructure.FollowerImplementations.Repositories;

public class FollowerRepository(DapperContext context) : IFollowerRepository
{
    public Task<bool> ExistsAsync(UserId followerId, UserId followeeId)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Follower follower)
    {
        throw new NotImplementedException();
    }
}