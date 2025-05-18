using Polls.Domain.Base.Events;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.ProfileAggregate.Events;

public sealed record ProfileCreated(ProfileId ProfileId, Username Username, UserId UserId) : DomainEvent;
