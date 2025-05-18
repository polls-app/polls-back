using Polls.Domain.Base.Events;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.UserAggregate.Events;

public sealed record UserRegistered(Email Email) : DomainEvent;
