using Polls.Domain.Base.ValueObjects;

namespace Polls.Domain.PollAggregate.ValueObjects;

public sealed class CategoryId(Guid value) : StronglyTypedId<CategoryId>(value)
{
    public static CategoryId New() => new CategoryId(Guid.NewGuid());
}