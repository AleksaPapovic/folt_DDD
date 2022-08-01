﻿using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.Domain.Events
{
    public class OrderCancelled : DomainEvent
    {
        public Guid CustomerId { get; private set; }

        public string EventType { get; private set; }

        public OrderCancelled(Guid orderId, Guid customerId) : base(orderId)
        {
            CustomerId = customerId;
            EventType = "OrderCancelled";
        }
    }
}
