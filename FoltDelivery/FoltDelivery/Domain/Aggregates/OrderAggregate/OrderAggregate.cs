using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Aggregate;
using FoltDelivery.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ShippingCost ShippingCost { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid, OrderItem> OrderItems { get; set; }
        public int LogicalDeleted { get; set; }

        public OrderAggregate() : base(Guid.NewGuid()) { }

        public OrderAggregate(OrderSnapshot snapshot) : base(snapshot.Id)
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
            if (orderDTO.Id == new Guid())
            {
                orderDTO.Id = Guid.NewGuid();
            }
            Version = orderDTO.Version;
            InitialVersion = orderDTO.Version;
            RestaurantId = orderDTO.RestaurantId;
            CustomerId = orderDTO.CustomerId;
            DeliveryId = orderDTO.DeliveryId;
            Address = orderDTO.Address;
            OrderItems = orderDTO.OrderItems;
            DateAndTime = orderDTO.DateAndTime;
            Status = orderDTO.Status;
            ShippingCost = orderDTO.ShippingCost;
            orderDTO.Price = CalculateOrderPrice();
            Causes(new OrderCreated(orderDTO));
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
            Version = orderCreated.Version;
            InitialVersion = orderCreated.Version;
            DeliveryId = orderCreated.DeliveryId;
            Price = new Money(orderCreated.Price.Amount);
            Address = orderCreated.Address;
            OrderItems = orderCreated.ConvertToOrderItemMap(orderCreated.OrderItems);
            DateAndTime = orderCreated.DateAndTime;
            Status = orderCreated.Status;
        }
        private void When(OrderCancelled orderCancelled)
        {
            Id = orderCancelled.Id;
            CustomerId = orderCancelled.CustomerId;
        }
        private void AddItem(OrderItemsUpdateDTO newItem)
        {
            Causes(new ItemAdded(newItem));
        }

        private void RemoveItem(OrderItemsUpdateDTO removedItem)
        {
            Causes(new ItemRemoved(removedItem));
        }

        private Money CalculateOrderPrice()
        {
            Money cost = new Money();
            if (OrderItems == null)
            {
                // Exception ako nema item-a
                cost = cost.Add(new Money(0));
            }

            else
            {
                foreach (var article in OrderItems)
                {
                    cost = cost.Add(new Money(article.Value.Quantity * article.Value.Cost.Amount));
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
