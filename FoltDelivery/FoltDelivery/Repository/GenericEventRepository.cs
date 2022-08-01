using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.Repository
{
    public class GenericEventRepository<T, S> : IGenericEventRepository<T, S> where T : EventSourcedAggregate where S : Snapshot
    {
        private readonly IEventStore _eventStore;

        public GenericEventRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public T FindBy(Guid aggregateId)
        {
            var streamName = StreamNameFor(aggregateId);

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            var snapshot = _eventStore.GetLatestSnapshot<S>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            T aggregate = null;
            if (snapshot != null)
            {
                aggregate = (T)Activator.CreateInstance(typeof(T), snapshot);
            }
            else
            {
                aggregate = (T)Activator.CreateInstance(typeof(T), null);
            }
            
            foreach (var @event in stream)
            {
                aggregate.Apply(@event);
            }

            return aggregate;
        }

        public void Add(T aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);

            _eventStore.CreateNewStream(streamName, aggregate.Changes);
        }

        public void Save(T aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);
            var expectedVersion = GetExpectedVersion(aggregate.InitialVersion);
            _eventStore.AppendEventsToStream(streamName, aggregate.Changes, expectedVersion);
        }

        private int? GetExpectedVersion(int expectedVersion)
        {
            if (expectedVersion == 0)
            {
                // first time the aggregate is stored there is no expected version
                return null;
            }
            else
            {
                return expectedVersion;
            }
        }

        public void SaveSnapshot(S snapshot, T aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);
            _eventStore.AddSnapshot<S>(streamName, snapshot);
        }

        private string StreamNameFor(Guid id)
        {
            // Stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(T).Name, id);
        }
    }
}
