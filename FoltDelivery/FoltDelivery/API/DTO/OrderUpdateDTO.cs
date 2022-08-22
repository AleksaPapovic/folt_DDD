using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class OrderUpdateDTO : Entity
    {
        public Guid OrderItemId { get; set; }
        public Guid CustomerId { get; set; }
        public Money Price { get; set; }
        public Dictionary<Guid, OrderItem> OrderItems { get; set; }
        public int Version { get; set; }
        public OrderUpdateDTO(Guid id) : base(id) { }
    }
}
