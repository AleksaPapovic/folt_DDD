using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Model.Enums;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class OrderDTO
    {
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public Money Price { get; set; }
        public ShippingCost ShippingCost { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid, Money> OrderItems { get; set; }
        public int LogicalDeleted { get; set; }
    }
}
