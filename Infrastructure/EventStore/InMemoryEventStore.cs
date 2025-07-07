using Domain.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Infrastructure.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentDictionary<Guid, List<IEvent>> _store = new();

        public Task SaveAsync(IEvent @event, CancellationToken cancellationToken = default)
        {
            var list = _store.GetOrAdd(@event.AggregateId, _ => new List<IEvent>());
            list.Add(@event);
            return Task.CompletedTask;
        }

        public void Save(IEvent @event)
        {
            var list = _store.GetOrAdd(@event.AggregateId, _ => new List<IEvent>());
            list.Add(@event);
        }

        public IEnumerable<IEvent> GetEvents(Guid aggregateId)
        {
            return _store.TryGetValue(aggregateId, out var events)
                ? events
                : Enumerable.Empty<IEvent>();
        }
    }
}