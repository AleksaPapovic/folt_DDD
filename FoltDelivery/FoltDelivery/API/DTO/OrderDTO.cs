using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure;
using FoltDelivery.Model.Enums;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class OrderDTO : Entity
    {
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public Money Price { get; set; }
        public ShippingCost ShippingCost { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid, int> OrderQuantities { get; set; }
        public Dictionary<Guid, OrderItem> OrderItems { get; set; }
        public int Version { get; protected set; }
        public int InitialVersion { get; protected set; }

        public OrderDTO() : base(Guid.NewGuid())
        {
            OrderItems = new Dictionary<Guid, OrderItem>();
        }
        public OrderDTO(Guid id) : base(id)
        {
            OrderItems = new Dictionary<Guid, OrderItem>();
        }

        internal Dictionary<Guid, OrderItemDTO> ConvertToOrderItemDTOMap(Dictionary<Guid, OrderItem> orderItems)
        {
            Dictionary<Guid, OrderItemDTO> orderItemsDTO = new Dictionary<Guid, OrderItemDTO>();
            foreach (var orderItem in orderItems)
            {
                orderItemsDTO.Add(orderItem.Key, new OrderItemDTO(orderItem.Value));
            }
            return orderItemsDTO;
        }

    }
}
