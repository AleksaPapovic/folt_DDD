using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.Product;
using FoltDelivery.Infrastructure;
using FoltDelivery.Model.Enums;

namespace FoltDelivery.Domain.Aggregates.Order
{
    public class OrderSnapshot:Snapshot
    {
        public int RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public Money Price { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid,Money> OrderItems { get; set; }
        public int LogicalDeleted { get; set; }

        public OrderSnapshot(Guid id) : base(id)
        {
        }
    }
}
