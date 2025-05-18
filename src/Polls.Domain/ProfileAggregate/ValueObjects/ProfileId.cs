using Polls.Domain.Base.ValueObjects;

namespace Polls.Domain.ProfileAggregate.ValueObjects;

public sealed class ProfileId(Guid value) : StronglyTypedId<ProfileId>(value)
{
    public static ProfileId New() => new ProfileId(Guid.NewGuid());
}