using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.EventStore;

namespace FoltDelivery.API.Repository
{
    public interface IOrderRepository : IGenericEventRepository<Order, OrderSnapshot>
    {
        public Task<List<Guid>> GetSuggestedFromAllOrders(OrderInCartDTO order);

        public Task<List<Guid>> GetSuggestedFromPersonalOrders(OrderInCartDTO order);
    }
}
