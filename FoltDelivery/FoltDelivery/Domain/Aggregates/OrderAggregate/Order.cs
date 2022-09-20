using FoltDelivery.API.DTO;
using FoltDelivery.API.Mapper;
using FoltDelivery.Core.Domain;
using FoltDelivery.Core.Domain.Aggregate;
using FoltDelivery.Core.Enums;
using FoltDelivery.Domain.Aggregates.OrderAggregate.Events;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class Order : EventSourcedAggregate
    {
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateAndTime { get; set; }
        public Money Price { get; set; }
        public ShippingCost ShippingCost { get; set; }
        public virtual Address Address { get; set; }
        public Dictionary<Guid, OrderItem> OrderItems { get; set; }
        public bool LogicalDeleted { get; set; }

        public Order() : base(Guid.NewGuid()) { }

        public Order(OrderSnapshot snapshot) : base(snapshot.Id)
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

        public Order(OrderDTO orderDTO)
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
            When(@event);
        }

        public override void When(DomainEvent @event)
        {
            Version += 1;
            InitialVersion = Version - 1;
            switch (@event)
            {
                case OrderCreated orderCreated:
                    Apply(orderCreated);
                    break;
                case OrderCancelled orderCancelled:
                    Apply(orderCancelled);
                    break;
                case ItemAdded orderItemAdded:
                    Apply(orderItemAdded);
                    break;
                case ItemRemoved orderItemRemoved:
                    Apply(orderItemRemoved);
                    break;
            }

        }

        private void Apply(OrderCreated orderCreated)
        {
            Id = orderCreated.Id;
            CustomerId = orderCreated.CustomerId;
            RestaurantId = orderCreated.RestaurantId;
            DeliveryId = orderCreated.DeliveryId;
            Price = new Money(orderCreated.Price.Amount);
            Address = orderCreated.Address;
            OrderItems = MapperProfile.ConvertToOrderItemMap(orderCreated.OrderItems);
            DateAndTime = orderCreated.DateAndTime;
            Status = orderCreated.Status;
            Version = 0;
            InitialVersion = 0;
        }
        private void Apply(OrderCancelled orderCancelled)
        {
            Id = orderCancelled.Id;
            CustomerId = orderCancelled.CustomerId;
        }

        private void Apply(ItemAdded orderItemAdded)
        {
            Id = orderItemAdded.Id;
            CustomerId = orderItemAdded.CustomerId;
            Price = new Money(orderItemAdded.Price.Amount);
            OrderItems = MapperProfile.ConvertToOrderItemMap(orderItemAdded.OrderItems);
        }

        private void Apply(ItemRemoved orderItemRemoved)
        {
            Id = orderItemRemoved.Id;
            CustomerId = orderItemRemoved.CustomerId;
            Price = new Money(orderItemRemoved.Price.Amount);
            OrderItems = MapperProfile.ConvertToOrderItemMap(orderItemRemoved.OrderItems);
        }
        internal void AddItem(OrderUpdateDTO newItem)
        {
            Price = Price.Add(newItem.Price);
            OrderItems = newItem.OrderItems;
            newItem.Price = new Money(Price.Amount);
            Causes(new ItemAdded(newItem));
        }

        internal void RemoveItem(OrderUpdateDTO removedItem)
        {
            Price = Price.Subtract(removedItem.Price);
            OrderItems = removedItem.OrderItems;
            removedItem.Price = new Money(Price.Amount);
            Causes(new ItemRemoved(removedItem));
        }

        internal void UpdateOrderItems(Guid itemId, bool isAdd)
        {
            if (isAdd == true)
            {
                OrderItems[itemId].Quantity += 1;
            }
            else
            {
                if (OrderItems[itemId].Quantity != 0)
                {
                    OrderItems[itemId].Quantity -= 1;
                }
            }
        }

        internal void UpdateIfSuggestedItem(ProductDTO newItem)
        {
            if (!OrderItems.TryGetValue(newItem.Id, out _))
            {
                OrderItems[newItem.Id] = new OrderItem(newItem, 0);
            }
        }

        private Money CalculateOrderPrice()
        {
            Money cost = new Money();
            if (OrderItems == null)
            {
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
