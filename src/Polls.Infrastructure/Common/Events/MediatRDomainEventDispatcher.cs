using MediatR;
using Polls.Domain.Base.Events;

namespace Polls.Infrastructure.Common.Events;

public class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
            await mediator.Publish(domainEvent);
    }
}