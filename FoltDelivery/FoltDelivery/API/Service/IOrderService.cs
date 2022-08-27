using System;
using System.Threading.Tasks;
using FoltDelivery.API.DTO;
using FoltDelivery.Domain.Aggregates.OrderAggregate;

namespace FoltDelivery.API.Service
{
    public interface IOrderService
    {
        public OrderAggregate GetOrder(Guid orderId);
        public OrderDTO CreateOrder(OrderDTO newOrder);

        public void GetSuggestedFromAll(string productId);
    }
}
