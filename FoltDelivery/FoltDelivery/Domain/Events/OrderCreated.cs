using System;
using System.Collections.Generic;
using AutoMapper;
using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure;
using FoltDelivery.Model.Enums;

namespace FoltDelivery.Domain.Events
{
    public class OrderCreated : DomainEvent
    {
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public MoneyDTO Price { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid, OrderItemDTO> OrderItems { get; set; }
        public OrderCreated() { }
        public OrderCreated(OrderDTO orderDTO) :base(orderDTO.Id, "OrderCreated")
        {
            RestaurantId = orderDTO.RestaurantId;
            CustomerId = orderDTO.CustomerId;
            DeliveryId = orderDTO.DeliveryId;
            Price =  new MoneyDTO(orderDTO.Price.Amount);
            OrderItems = orderDTO.ConvertToOrderItemDTOMap(orderDTO.OrderItems);
            DateAndTime = orderDTO.DateAndTime;
            Status = OrderStatus.CREATED;
            Version = 0;
            Address = orderDTO.Address;
        }

        public Dictionary<Guid, OrderItem> ConvertToOrderItemMap(Dictionary<Guid, OrderItemDTO> orderItemsDTO)
        {
            Dictionary<Guid, OrderItem> orderItems = new Dictionary<Guid, OrderItem>();
            foreach (var orderItemDTO in orderItemsDTO)
            {
                orderItems.Add(orderItemDTO.Key, new OrderItem(orderItemDTO.Value));
            }
            return orderItems;
        }

    }
}
