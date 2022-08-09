using EventStore.ClientAPI;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.Repository
{
    public class OrderRepository : GenericEventRepository<OrderAggregate,OrderSnapshot>, IOrderRepository
    {
        private readonly IEventStore _eventStore;

        public OrderRepository(IEventStore eventStore) : base(eventStore)
        {
            _eventStore = eventStore;
        }

        public OrderAggregate FindBy(Guid id)
        {
            var streamName = StreamNameFor(id);

            long fromEventNumber = 0;
            int toEventNumber = int.MaxValue;

            var snapshot = _eventStore.GetLatestSnapshot<OrderSnapshot>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber).Result;

            OrderAggregate order = null;
            if (snapshot != null)
            {
                order = new OrderAggregate(snapshot);
            }
            else
            {
                order = new OrderAggregate();
            }


            foreach (var @event in stream)
            {
                order.Apply(@event);
            }

             return order;
        }

        public static int i = 0;
        public void Add(OrderAggregate order)
        {
            string streamName = StreamNameFor(order.Id);

            _eventStore.CreateNewStream(streamName, order.Changes);

            _eventStore.AppendEventsToStream(streamName, order.Changes, ExpectedVersion.Any);
        }

        public void Save(OrderAggregate order)
        {
            var streamName = StreamNameFor(order.Id);
            var expectedVersion = GetExpectedVersion(order.InitialVersion);
            _eventStore.AppendEventsToStream(streamName, order.Changes, expectedVersion);
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

        public void SaveSnapshot(OrderSnapshot snapshot, OrderAggregate order)
        {
            var streamName = StreamNameFor(order.Id);
            _eventStore.AddSnapshot<OrderSnapshot>(streamName, snapshot);
        }

        //private string StreamNameFor(Guid id)
        //{
        //    // Stream per-aggregate: {AggregateType}-{AggregateId}
        //    return string.Format("{0}-{1}", typeof(OrderAggregate).Name, id);
        //}




    }
}
