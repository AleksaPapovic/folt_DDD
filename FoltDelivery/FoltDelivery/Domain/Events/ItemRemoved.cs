using FoltDelivery.API.Mapper;
using FoltDelivery.API.DTO;
using FoltDelivery.Infrastructure;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Domain.Events
{
    public class ItemRemoved : DomainEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid ItemId { get; private set; }
        public MoneyDTO Price { get; set; }
        public Dictionary<Guid, OrderItemDTO> OrderItems { get; set; }
        public ItemRemoved() { }
        public ItemRemoved(OrderUpdateDTO orderUpdate) : base(orderUpdate.Id, "ItemRemoved")
        {
            CustomerId = orderUpdate.CustomerId;
            ItemId = orderUpdate.OrderItemId;
            Price = new MoneyDTO(orderUpdate.Price.Amount);
            OrderItems = MapperProfile.ConvertToOrderItemDTOMap(orderUpdate.OrderItems);
        }
    }
}
