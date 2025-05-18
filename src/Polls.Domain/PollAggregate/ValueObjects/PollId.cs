using Polls.Domain.Base.ValueObjects;

namespace Polls.Domain.PollAggregate.ValueObjects;

public sealed class PollId(Guid value) : StronglyTypedId<PollId>(value)
{
    public static PollId New() => new PollId(Guid.NewGuid());
}