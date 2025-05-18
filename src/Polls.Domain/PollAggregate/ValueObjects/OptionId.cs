using Polls.Domain.Base.ValueObjects;

namespace Polls.Domain.PollAggregate.ValueObjects;

public sealed class OptionId(Guid value) : StronglyTypedId<OptionId>(value)
{
    public static OptionId New() => new OptionId(Guid.NewGuid());
}