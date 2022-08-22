using System;
using System.Collections.Generic;
using FoltDelivery.API.Mapper;
using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Events
{
    public class ItemAdded : DomainEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid ItemId { get; private set; }
        public MoneyDTO Price { get; set; }
        public Dictionary<Guid, OrderItemDTO> OrderItems { get; set; }
        public ItemAdded() { }
        public ItemAdded(OrderUpdateDTO orderUpdate) : base(orderUpdate.Id, "ItemAdded")
        {
            CustomerId = orderUpdate.CustomerId;
            ItemId = orderUpdate.OrderItemId;
            Price = new MoneyDTO(orderUpdate.Price.Amount);
            OrderItems = MapperProfile.ConvertToOrderItemDTOMap(orderUpdate.OrderItems);
        }

    }
}
