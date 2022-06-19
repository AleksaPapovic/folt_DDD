using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
    public class OrderCancelled : DomainEvent
    {
        public Guid CustomerId { get; private set; }

        public string EventType { get; private set; } = "OrderCancelled";


        public OrderCancelled(Guid orderId, Guid customerId) : base(orderId)
        {
            CustomerId = customerId;

        }
    }
}
