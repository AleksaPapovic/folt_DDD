using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
    public class OrderCreated : DomainEvent
    {
        public Guid CustomerId { get; private set; }

        public string EventType { get; private set; } 


        public OrderCreated(Guid orderId, Guid customerId):base(orderId)
        {
            CustomerId = customerId;
            EventType = "OrderCreated";

        }
    }
}
