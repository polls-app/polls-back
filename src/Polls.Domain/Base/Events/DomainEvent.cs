namespace Polls.Domain.Base.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}