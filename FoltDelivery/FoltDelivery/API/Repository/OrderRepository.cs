using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.Repository
{
    public class OrderRepository : GenericEventRepository<OrderAggregate, OrderSnapshot>, IOrderRepository
    {
        public OrderRepository(IEventStore eventStore) : base(eventStore) { }

        public List<OrderAggregate> GetOrdersInCart(List<Guid> orderIds)
        {
            List<OrderAggregate> orderAggregates = new List<OrderAggregate>();
            //foreach kroz snapshot-e
            return null;

        }


    }
}
