using FoltDelivery.Model.Enums;
using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Model
{
    public class Order : EventSourcedAggregate
    {

        public int RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public String DeliveryId { get; set; }
        public int Commented { get; set; }

        public OrderStatus Status { get; set; }
        public int LogicalDeleted { get; set; }

        public DateTime DateAndTime { get; set; }
        public float Price { get; set; }

        public List<OrderItem> OrderItems;


        public Order(Guid id, int restaurantId,
            DateTime dateAndTime, float price, Guid customerId, String deliveryId,
            OrderStatus status, int logicalDeleted, int commented) : base(id)
        {
            RestaurantId = restaurantId;
            DateAndTime = dateAndTime;
            Price = price;
            CustomerId = customerId;
            DeliveryId = deliveryId;
            Status = status;
            LogicalDeleted = logicalDeleted;
            Commented = commented;
            OrderItems = new List<OrderItem>();
        }


        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version = Version++;
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



    }
}
