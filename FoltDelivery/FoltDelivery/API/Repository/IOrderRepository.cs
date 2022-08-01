using System;
using FoltDelivery.Domain.Aggregates.Order;

namespace FoltDelivery.API.Repository
{
    public interface IOrderRepository : IGenericEventRepository<Order, OrderSnapshot>
    {
        public Order FindBy(Guid id);
        public void Add(Order order);
        public void Save(Order order);
        public void SaveSnapshot(OrderSnapshot snapshot, Order order);
    }
}
