using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.OrderAggregate;

namespace FoltDelivery.API.Repository
{
    public interface IOrderRepository : IGenericEventRepository<OrderAggregate, OrderSnapshot>
    {
        public void GetSuggestedFromAll(string productId);
    }
}
