using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Core.Enums;
using System;
using FoltDelivery.Core.Domain;

namespace FoltDelivery.API.DTO
{
    public class OrderItemDTO : Entity
    {
        public int Quantity { get; set; }
        public MoneyDTO Cost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ProductType ProductType { get; set; }

        public OrderItemDTO():base(Guid.NewGuid()) { }

        public OrderItemDTO(OrderItem orderItem) : base(orderItem.Id)
        {
            Quantity = orderItem.Quantity;
            Cost = new MoneyDTO(orderItem.Cost.Amount);
            Name = orderItem.Name;
            Description = orderItem.Description;
            Image = orderItem.Image;
            ProductType = orderItem.ProductType;
        }
    }

}
