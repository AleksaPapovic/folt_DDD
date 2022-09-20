using FoltDelivery.Core.Domain;
using FoltDelivery.Core.Domain.Aggregate;
using FoltDelivery.Core.EventStore;
using System;

namespace FoltDelivery.Infrastructure.EventStore
{
    public class GenericEventRepository<T, S> : IGenericEventRepository<T, S> where T : EventSourcedAggregate, new() where S : Snapshot
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

            S snapshot = _eventStore.GetLatestSnapshot<S>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1;
            }

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber).Result;

            T aggregate;
            if (snapshot != null)
            {
                aggregate = (T)Activator.CreateInstance(typeof(T), new object[] { snapshot });
            }
            else
            {
                aggregate = new T();
            }

            foreach (var @event in stream)
            {
                aggregate.When(@event);
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
            var expectedVersion = aggregate.InitialVersion;
            _eventStore.AppendEventsToStream(streamName, aggregate.Changes, expectedVersion);
        }

        public void SaveSnapshot(S snapshot, T aggregate)
        {
            var streamName = StreamNameFor(aggregate.Id);
            _eventStore.AddSnapshot<S>(streamName, snapshot);
        }

        public string StreamNameFor(Guid aggregateId)
        {
            return string.Format("{0}-{1}", typeof(T).Name, aggregateId);
        }
    }
}
