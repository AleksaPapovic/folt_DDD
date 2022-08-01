using FoltDelivery.Domain.Aggregates.Order;

namespace FoltDelivery.Service
{
    public interface IOrderService
    {
        public Order CreateOrder(Order newOrder);
    }
}
