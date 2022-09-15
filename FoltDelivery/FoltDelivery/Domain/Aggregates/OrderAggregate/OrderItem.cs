using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure;
using FoltDelivery.Model.Enums;
using System;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        public Money Cost { get; set; }
        public string Name  { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }

        public OrderItem(OrderItemDTO orderItemDTO) : base(orderItemDTO.Id)
        {
            Quantity = orderItemDTO.Quantity;
            Cost = new Money(orderItemDTO.Cost.Amount);
            Name = orderItemDTO.Name;
            Description = orderItemDTO.Description;
            ProductType = orderItemDTO.ProductType;
        }


        public OrderItem(ProductDTO product, int quantity) : base(product.Id)
        {
            Quantity = quantity;
            Cost = new Money(product.Price.Amount);
            Name = product.Name;
            Description = product.Description;
            ProductType = product.Type;
        }

        public OrderItem(Guid orderItemId, int quantity, Money cost) : base(orderItemId)
        {
            Quantity = quantity;
            Cost = cost;
        }
    }
}
