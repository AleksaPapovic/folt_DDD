using FoltDelivery.Core.Domain;
using FoltDelivery.Core.Enums;
using System;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class Cart : Entity
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public CartStatus Status { get; set; }
        public bool LogicalDeleted { get; set; }

        public Cart(Guid id) : base(id) { }

        public Cart(Guid id, Guid orderId, Guid customerId, float price, CartStatus cartStatus,
            bool logicalDeleted) : base(id)
        {
            Status = cartStatus;
            OrderId = orderId;
            CustomerId = customerId;
            LogicalDeleted = logicalDeleted;
        }

        public Cart(Cart cart) : base(cart.Id)
        {
            Id = cart.Id;
            OrderId = cart.OrderId;
            CustomerId = cart.CustomerId;
            Status = cart.Status;
            LogicalDeleted = cart.LogicalDeleted;
        }

    }
}
