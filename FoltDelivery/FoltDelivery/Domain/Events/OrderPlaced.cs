using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
        public class OrderPlaced : DomainEvent
    {
        public Guid CustomerId { get; private set; }

        public string EventType { get; private set; }


        public OrderPlaced(Guid orderId, Guid customerId) : base(orderId)
        {
            CustomerId = customerId;
            EventType = "OrderPlaced";

        }
    }
}
