using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.API.DTO
{
    public class OrderItemDTO : Entity
    {
        public int Quantity { get; set; }
        public MoneyDTO Cost { get; set; }

        public OrderItemDTO():base(Guid.NewGuid()) { }
        public OrderItemDTO(Guid orderItemId, int quantity, MoneyDTO cost) : base(orderItemId)
        {
            Quantity = quantity;
            Cost = cost;
        }

        public OrderItemDTO(OrderItem orderItem) : base(orderItem.Id)
        {
            Quantity = orderItem.Quantity;
            Cost = new MoneyDTO(orderItem.Cost.Amount);
        }
    }

}
