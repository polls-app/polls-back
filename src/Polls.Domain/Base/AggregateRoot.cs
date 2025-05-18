using Polls.Domain.Base.Events;

namespace Polls.Domain.Base;

public abstract class AggregateRoot
{
    private readonly IList<IDomainEvent> _events = [];

    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

    public void ClearEvents() => _events.Clear();
}