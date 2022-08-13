using System;
using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
    public class ItemAdded : DomainEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid ItemId { get; private set; }

        public ItemAdded(OrderItemsUpdateDTO newItem) : base(newItem.OrderId, "ItemAdded")
        {
            ItemId = newItem.Id;
            CustomerId = newItem.CustomerId;
        }
    }
}
