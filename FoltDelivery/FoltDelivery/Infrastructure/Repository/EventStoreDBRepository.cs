using EventStore.Client;
using System;
using FoltDelivery.Infrastructure.Aggregate;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using FoltDelivery.Infrastructure.Events;
using System.Collections.Generic;

namespace FoltDelivery.Infrastructure.Repository
{
    public class EventStoreDBRepository<T> : IEventStoreDBRepository<T> where T : class, IAggregate
    {
        private readonly EventStoreClient eventStore;

        public EventStoreDBRepository(EventStoreClient eventStore)
        {
            this.eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public Task<T> Find(Guid id, CancellationToken cancellationToken) =>
            eventStore.AggregateStream<T>(
                id,
                cancellationToken
            );

        public async Task<ulong> Add(T aggregate,
            CancellationToken ct = default)
        {
            var result = await eventStore.AppendToStreamAsync(
                StreamNameMapper.ToStreamId<T>(aggregate.Id),
                StreamState.NoStream,
                GetEventsToStore(aggregate),
                cancellationToken: ct
            );
            return result.NextExpectedStreamRevision;
        }

        public async Task<ulong> Update(T aggregate, ulong? expectedRevision = null,
            CancellationToken ct = default)
        {
            var eventsToAppend = GetEventsToStore(aggregate);
            var nextVersion = expectedRevision ?? (ulong)(aggregate.Version - (int) eventsToAppend.Count);

            var result = await eventStore.AppendToStreamAsync(
                StreamNameMapper.ToStreamId<T>(aggregate.Id),
                nextVersion,
                eventsToAppend,
                cancellationToken: ct
            );
            return result.NextExpectedStreamRevision;
        }

        public Task<ulong> Delete(T aggregate, ulong? expectedRevision = null,
            CancellationToken ct = default) =>
            Update(aggregate, expectedRevision, ct);

        private static List<EventData> GetEventsToStore(T aggregate)
        {
            var events = aggregate.DeleteUncommittedEvents();

            return events
                .Select(@event => @event.ToJsonEventData())
                .ToList();
        }
    }
}
