using Polls.Domain.PollAggregate.Entities;
using Polls.Domain.PollAggregate.Repositories;
using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Infrastructure.PollImplementations.Repositories;

public class HashtagRepository : IHashtagRepository
{
    public Task<HashTag?> GetById(HashTagId hashTagId)
    {
        throw new NotImplementedException();
    }
}