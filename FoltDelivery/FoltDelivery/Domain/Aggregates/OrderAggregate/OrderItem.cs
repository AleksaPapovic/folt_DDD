using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        public Money Cost { get; set; }

        public OrderItem(OrderItemDTO orderItemDTO) : base(orderItemDTO.Id)
        {
            Quantity = orderItemDTO.Quantity;
            Cost = new Money(orderItemDTO.Cost.Amount);
        }

        public OrderItem(Guid orderItemId, int quantity, Money cost) : base(orderItemId)
        {
            Quantity = quantity;
            Cost = cost;
        }
    }
}
