using Polls.Domain.PollAggregate;
using Polls.Domain.PollAggregate.Repositories;

namespace Polls.Infrastructure.PollImplementations.Repositories;

public class PollRepository : IPollRepository
{
    public Task Add(Poll poll)
    {
        throw new NotImplementedException();
    }
}