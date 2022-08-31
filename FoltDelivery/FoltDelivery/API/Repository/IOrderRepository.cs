using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;

namespace FoltDelivery.API.Repository
{
    public interface IOrderRepository : IGenericEventRepository<OrderAggregate, OrderSnapshot>
    {
        public Task<List<Guid>> GetSuggestedFromAllOrders(OrderInCartDTO order);

        public Task<List<Guid>> GetSuggestedFromPersonalOrders(OrderInCartDTO order);
    }
}
