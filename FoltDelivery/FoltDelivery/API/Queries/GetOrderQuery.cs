using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.Queries;
using System;

namespace FoltDelivery.API.Queries
{
    public record GetOrderQuery(Guid OrderId) : IQuery<OrderAggregate>
    {
        public static GetOrderQuery Create(Guid orderId)
        {
            if (orderId == null)
                throw new ArgumentOutOfRangeException(nameof(orderId));

            return new GetOrderQuery(orderId);
        }
    }
}
