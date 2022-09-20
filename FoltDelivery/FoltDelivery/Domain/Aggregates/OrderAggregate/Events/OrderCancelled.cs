using FoltDelivery.Core.Domain;
using System;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate.Events
{
    public class OrderCancelled : DomainEvent
    {
        public Guid CustomerId { get; private set; }

        public OrderCancelled(Guid orderId, Guid customerId) : base(orderId, "OrderCancelled")
        {
            CustomerId = customerId;
        }
    }
}
