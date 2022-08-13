using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.Queries;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.Queries
{
    public record GetOrdersInCartQuery(Guid userId) : IQuery<List<OrderAggregate>>
    {
        public static GetOrdersInCartQuery Create(Guid userId)
        {
            return new GetOrdersInCartQuery(userId);
        }
    }
}
