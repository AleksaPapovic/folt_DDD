using FoltDelivery.Domain.Aggregates.Order;

namespace FoltDelivery.API.Service
{
    public interface IOrderService
    {
        public Order CreateOrder(Order newOrder);
    }
}
