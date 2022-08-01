using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
    public class OrderCreated : DomainEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public OrderCreated(Guid orderId, Guid customerId):base(orderId, "OrderCreated")
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
