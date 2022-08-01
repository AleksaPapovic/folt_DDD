using System;
using System.Collections.Generic;
using System.Linq;
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
        public Money Price { get; set; }
        public ShippingCost ShippingCost { get; set;}
        public virtual Address Address { get; set; }
        public Dictionary<Guid,Money> OrderItems { get; set; }
        public int LogicalDeleted { get; set; }

        public Order():base(new Guid()){}

        public Order(OrderSnapshot snapshot):base(snapshot.Id)
        {
            Version = snapshot.Version;
            InitialVersion = snapshot.Version;
            RestaurantId = snapshot.RestaurantId;
            CustomerId = snapshot.CustomerId;
            DeliveryId = snapshot.DeliveryId;
            Price = new Money(snapshot.Price.Amount);
            Address = new Address(snapshot.Address);
            OrderItems = snapshot.OrderItems;
            DateAndTime = snapshot.DateAndTime;
            Status = snapshot.Status;
            LogicalDeleted = snapshot.LogicalDeleted;
        }

        public Order(Guid id, int restaurantId, Guid customerId, String deliveryId,
            DateTime dateAndTime) : base(id)
        {
            RestaurantId = restaurantId;
            CustomerId = customerId;
            DeliveryId = deliveryId;
            Price = CalculateOrderPrice();
            OrderItems = new Dictionary<Guid, Money>();
            DateAndTime = dateAndTime;
            Status = OrderStatus.CREATED;
            Causes(new OrderCreated(id, customerId));
        }

        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
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
        private void AddItem(Guid orderId, Guid customerId, Guid itemId)
        {
            Causes(new ItemAdded(orderId, customerId,  itemId));
        }

       private void RemoveItem(Guid orderId, Guid customerId, Guid itemId)
        {
            Causes(new ItemRemoved(orderId, customerId, itemId));
        }

           private Money CalculateOrderPrice()
        {
            Money cost = new Money();
            foreach (var article in OrderItems)
            {
                cost = cost.Add(article.Value);
            }
            cost = cost.Add(new Money(this.ShippingCost.Price));
            return cost;
        }

           private bool ContainsArticle(Guid articleId)
           {
               return OrderItems.Any(x => x.Key == articleId);
           }

    }
}
