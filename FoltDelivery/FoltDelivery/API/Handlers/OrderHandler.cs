using System.Threading.Tasks;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.API.Handlers
{
    //public class OrderHandler : IEventHandler<OrderCreated>, IEventHandler<OrderPlaced>
    //{

    //    private readonly  IOrderRepository _orderRepository;

    //    public OrderHandler(IOrderRepository orderRepository)
    //    { 
    //        _orderRepository = orderRepository;
    //    }

    //    public Task HandleAsync(OrderCreated @event)
    //    {

    //        //Order order = new Order(newOrder);
    //        //_orderRepository.Add(order);
    //        throw new System.NotImplementedException();
    //    }

    //    public Task HandleAsync(OrderPlaced @event)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
