using Polls.Domain.Base.ValueObjects;

namespace Polls.Domain.PollAggregate.ValueObjects;

public sealed class HashTagId(Guid value) : StronglyTypedId<HashTagId>(value)
{
    public static HashTagId New() => new HashTagId(Guid.NewGuid());
}