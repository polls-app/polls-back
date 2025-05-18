using Polls.Domain.Base.ValueObjects;

namespace Polls.Domain.UserAggregate.ValueObjects;

public sealed class UserId(Guid value) : StronglyTypedId<UserId>(value)
{
    public static UserId New() => new UserId(Guid.NewGuid());
}