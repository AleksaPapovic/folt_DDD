using System;
using FoltDelivery.Domain.Aggregates.Order;
using FoltDelivery.Infrastructure.Queries;

namespace FoltDelivery.API.Queries
{
    public class GetOrderQuery:IQuery<Order>
    {
        public Guid OrderId { get; set; }
    }
}
