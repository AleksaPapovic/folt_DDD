using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.OrderAggregate;

namespace FoltDelivery.API.Repository
{
    public interface IOrderRepository : IGenericEventRepository<OrderAggregate, OrderSnapshot>
    {
        //public OrderAggregate FindBy(Guid id);
        //public void Add(OrderAggregate order);
        //public void Save(OrderAggregate order);
        //public void SaveSnapshot(OrderSnapshot snapshot, OrderAggregate order);
        public List<OrderAggregate> GetOrdersInCart(List<Guid> orderIds);
    }
}
