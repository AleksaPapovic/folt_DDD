using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Core.Queries;
using System;
using System.Collections.Generic;

namespace FoltDelivery.API.Queries
{
    public record GetOrdersInCartQuery(Guid userId) : IQuery<List<Order>>
    {
        public static GetOrdersInCartQuery Create(Guid userId)
        {
            return new GetOrdersInCartQuery(userId);
        }
    }
}
