using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Core.Enums;
using System;
using FoltDelivery.Core.Domain;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        public Money Cost { get; set; }
        public string Name  { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ProductType ProductType { get; set; }

        public OrderItem(OrderItemDTO orderItemDTO) : base(orderItemDTO.Id)
        {
            Quantity = orderItemDTO.Quantity;
            Cost = new Money(orderItemDTO.Cost.Amount);
            Name = orderItemDTO.Name;
            Description = orderItemDTO.Description;
            Image = orderItemDTO.Image;
            ProductType = orderItemDTO.ProductType;
        }


        public OrderItem(ProductDTO product, int quantity) : base(product.Id)
        {
            Quantity = quantity;
            Cost = new Money(product.Price.Amount);
            Name = product.Name;
            Description = product.Description;
            Image= product.Image;
            ProductType = product.Type;
        }

        public OrderItem(Guid orderItemId, int quantity, Money cost) : base(orderItemId)
        {
            Quantity = quantity;
            Cost = cost;
        }
    }
}
