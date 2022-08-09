using System;
using FoltDelivery.Infrastructure.Enums;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class Cart : Entity
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public CartStatus Status { get; set; }
        public int LogicalDeleted { get; set; }

        public Cart(Guid id) : base(id) { }

        public Cart(Guid id, Guid orderId, Guid customerId, float price, CartStatus cartStatus,
            int logicalDeleted): base(id)
        {
            Status = cartStatus;
            OrderId = orderId;
            CustomerId = customerId;
            LogicalDeleted = logicalDeleted;
        }

        public Cart(Cart cart):base(cart.Id)
        {
            Id = cart.Id;
            OrderId = cart.OrderId;
            CustomerId = cart.CustomerId;
            Status = cart.Status;
            LogicalDeleted = cart.LogicalDeleted;
        }

    }
}
