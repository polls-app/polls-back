namespace Polls.Domain.PollAggregate.Repositories;

public interface IPollRepository
{
    Task Add(Poll poll);
}