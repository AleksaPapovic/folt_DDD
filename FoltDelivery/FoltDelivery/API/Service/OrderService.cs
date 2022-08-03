using AutoMapper;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.Order;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Authorization;

namespace FoltDelivery.API.Service
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private IEventStore _eventStore;
        private IJwtUtils _jwtUtils;
        private IMapper _mapper;
        public OrderService() { }
        public OrderService(IOrderRepository orderRepository, IEventStore eventStore, IJwtUtils jwtUtils, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _eventStore = eventStore;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public Order CreateOrder(Order newOrder)
        {
            //Order order = new Order(newOrder);
            //_orderRepository.Add(order);
            return newOrder;
        }
    }
}
