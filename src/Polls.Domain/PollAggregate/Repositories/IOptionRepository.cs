using Polls.Domain.PollAggregate.Entities;
using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Domain.PollAggregate.Repositories;

public interface IOptionRepository
{
    Task<Option?> GetById(OptionId optionId);
}