using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.OrderAggregate;

namespace FoltDelivery.API.Repository
{
    public interface IOrderRepository : IGenericEventRepository<OrderAggregate, OrderSnapshot>
    {
        public List<OrderAggregate> GetOrdersInCart(List<Guid> orderIds);
    }
}
