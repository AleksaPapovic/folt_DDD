using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
    public class ItemAdded : DomainEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid ItemId { get; private set; }

        public ItemAdded(Guid orderId, Guid customerId, Guid itemId) : base(orderId, "ItemAdded")
        {
            ItemId = itemId;
            CustomerId = customerId;
        }
    }
}
