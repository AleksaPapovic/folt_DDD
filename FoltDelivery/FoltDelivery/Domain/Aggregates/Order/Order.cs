using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.Product;
using FoltDelivery.Domain.Events;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.Order
{
    public class Order : EventSourcedAggregate
    {
        public int RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public float Price { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid,int> OrderItems { get; set; }
        public int LogicalDeleted { get; set; }

        public Order():base(new Guid()){}

        public Order(OrderSnapshot snapshot):base(snapshot.Id)
        {
            Version = snapshot.Version;
            InitialVersion = snapshot.Version;
            RestaurantId = snapshot.RestaurantId;
            DateAndTime = snapshot.DateAndTime;
            Price = snapshot.Price;
            CustomerId = snapshot.CustomerId;
            DeliveryId = snapshot.DeliveryId;
            Status = snapshot.Status;
            LogicalDeleted = snapshot.LogicalDeleted;
            OrderItems = snapshot.OrderItems;
            //_credit = new Money(snapshot.Credit);
        }
        public Order(Guid id, int restaurantId,
            DateTime dateAndTime, float price, Guid customerId, String deliveryId,
            OrderStatus status, int logicalDeleted) : base(id)
        {
            RestaurantId = restaurantId;
            DateAndTime = dateAndTime;
            Price = price;
            CustomerId = customerId;
            DeliveryId = deliveryId;
            Status = status;
            LogicalDeleted = logicalDeleted;
            OrderItems = new Dictionary<Guid, int>();
            Causes(new OrderCreated(id, customerId));
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version = Version++;
        }
        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        private void When(OrderCreated orderCreated)
        {
            Id = orderCreated.Id;
            CustomerId = orderCreated.CustomerId;
        }
        private void When(OrderCancelled orderCancelled)
        {
            Id = orderCancelled.Id;
            CustomerId = orderCancelled.CustomerId;
        }
        public void AddItem(Guid orderId, Guid customerId, Guid itemId)
        {
            Causes(new ItemAdded(orderId, customerId,  itemId));
        }

        public void RemoveItem(Guid orderId, Guid customerId, Guid itemId)
        {
            Causes(new ItemRemoved(orderId, customerId, itemId));
        }
    }
}
