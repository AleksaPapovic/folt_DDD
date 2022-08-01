using FoltDelivery.Domain.Aggregates.Order;
using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.Repository
{
    public class OrderRepository : GenericEventRepository<Order,OrderSnapshot>, IOrderRepository
    {
        private readonly IEventStore _eventStore;

        public OrderRepository(IEventStore eventStore) : base(eventStore)
        {
            _eventStore = eventStore;
        }

        public Order FindBy(Guid id)
        {
            var streamName = StreamNameFor(id);

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            var snapshot = _eventStore.GetLatestSnapshot<OrderSnapshot>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }

            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            Order order = null;
            if (snapshot != null)
            {
                order = new Order(snapshot);
            }
            else
            {
                order = new Order();
            }


            foreach (var @event in stream)
            {
                order.Apply(@event);
            }

            return order;
        }


        public void Add(Order order)
        {
            var streamName = StreamNameFor(order.Id);

            _eventStore.CreateNewStream(streamName, order.Changes);
        }

        public void Save(Order order)
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

        public void SaveSnapshot(OrderSnapshot snapshot, Order order)
        {
            var streamName = StreamNameFor(order.Id);
            _eventStore.AddSnapshot<OrderSnapshot>(streamName, snapshot);
        }

        private string StreamNameFor(Guid id)
        {
            // Stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(Order).Name, id);
        }




    }
}
