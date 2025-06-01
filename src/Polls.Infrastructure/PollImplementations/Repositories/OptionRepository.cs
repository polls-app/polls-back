using Polls.Domain.PollAggregate.Entities;
using Polls.Domain.PollAggregate.Repositories;
using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Infrastructure.PollImplementations.Repositories;

public class OptionRepository : IOptionRepository
{
    public Task<Option?> GetById(OptionId optionId)
    {
        throw new NotImplementedException();
    }
}