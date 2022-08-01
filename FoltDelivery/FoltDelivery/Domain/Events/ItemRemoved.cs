using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.Domain.Events
{
    public class ItemRemoved : DomainEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid ItemId { get; private set; }

        public ItemRemoved(Guid orderId, Guid customerId, Guid itemId) : base(orderId, "ItemRemoved")
        {
            ItemId = itemId;
            CustomerId = customerId;
        }
    }
}
