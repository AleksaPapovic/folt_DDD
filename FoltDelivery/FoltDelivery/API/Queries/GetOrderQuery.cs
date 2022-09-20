using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Core.Queries;
using System;

namespace FoltDelivery.API.Queries
{
    public record GetOrderQuery(Guid OrderId) : IQuery<Order>
    {
        public static GetOrderQuery Create(Guid orderId)
        {
            if (orderId == new Guid())
                throw new ArgumentOutOfRangeException(nameof(orderId));

            return new GetOrderQuery(orderId);
        }
    }
}
