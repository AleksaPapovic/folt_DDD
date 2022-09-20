using FoltDelivery.API.DTO;
using FoltDelivery.API.Mapper;
using FoltDelivery.Core.Domain;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate.Events
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
