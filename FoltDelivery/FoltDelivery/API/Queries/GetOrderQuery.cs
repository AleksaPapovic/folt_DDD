using System;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.Queries;

namespace FoltDelivery.API.Queries
{
    public class GetOrderQuery:IQuery<OrderAggregate>
    {
        public Guid OrderId { get; set; }
    }
}
