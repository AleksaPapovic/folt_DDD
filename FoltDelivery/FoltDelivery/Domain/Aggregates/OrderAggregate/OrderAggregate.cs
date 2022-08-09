using System;
using System.Collections.Generic;
using System.Linq;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Domain.Events;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Aggregate;
using FoltDelivery.API.DTO;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class OrderAggregate : EventSourcedAggregate
    {
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public Money Price { get; set; }
        public ShippingCost ShippingCost { get; set;}
        public virtual Address Address { get; set; }
        public Dictionary<Guid,Money> OrderItems { get; set; }
        public int LogicalDeleted { get; set; }

        public OrderAggregate() : base(new Guid()) { }
        public OrderAggregate(Guid id):base(id){}

        public OrderAggregate(OrderSnapshot snapshot):base(snapshot.Id)
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

        public OrderAggregate(OrderDTO orderDTO)
        {
            Id = Guid.NewGuid();
            RestaurantId = orderDTO.RestaurantId;
            CustomerId = orderDTO.CustomerId;
            DeliveryId = orderDTO.DeliveryId;
            ShippingCost = orderDTO.ShippingCost;
            Price = CalculateOrderPrice();
            OrderItems = orderDTO.OrderItems;
            DateAndTime = orderDTO.DateAndTime;
            Status = OrderStatus.CREATED;
            Address = orderDTO.Address;
            Causes(new OrderCreated(Id, CustomerId,RestaurantId));
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
            RestaurantId = orderCreated.RestaurantId;
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
            if (OrderItems == null) { cost = cost.Add(new Money(0)); }

            else
            {
                foreach (var article in OrderItems)
                {
                    cost = cost.Add(article.Value);
                }
            }
            if (ShippingCost == null) ShippingCost = new ShippingCost(0);
            else { cost = cost.Add(new Money(this.ShippingCost.Price)); }
            return cost;
        }

           private bool ContainsArticle(Guid articleId)
           {
               return OrderItems.Any(x => x.Key == articleId);
           }

    }
}
