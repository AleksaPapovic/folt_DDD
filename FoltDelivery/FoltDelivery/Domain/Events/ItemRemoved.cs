using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.Domain.Events
{
    public class ItemRemoved : DomainEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid ItemId { get; private set; }

        public ItemRemoved(OrderItemsUpdateDTO removedItem) : base(removedItem.OrderId, "ItemRemoved")
        {
            ItemId = removedItem.Id;
            CustomerId = removedItem.CustomerId;
        }
    }
}
