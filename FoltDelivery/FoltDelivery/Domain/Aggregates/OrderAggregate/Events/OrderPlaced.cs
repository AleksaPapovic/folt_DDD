using FoltDelivery.Core.Domain;
using System;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate.Events
{
    public class OrderPlaced : DomainEvent
    {
        public Guid CustomerId { get; private set; }

        public OrderPlaced(Guid orderId, Guid customerId) : base(orderId, "OrderPlaced")
        {
            CustomerId = customerId;
        }
    }
}
