using System;
using AutoMapper;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
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

        public OrderAggregate GetOrder(Guid orderId)
        {
            return _orderRepository.FindBy(orderId);
        }

        public OrderDTO CreateOrder(OrderDTO newOrder)
        {
            OrderAggregate order = new OrderAggregate(newOrder);
           _orderRepository.Add(order);
            return newOrder;
        }

        public void GetSuggestedFromAll(string productId) {
            _orderRepository.GetSuggestedFromAll(productId);
        }

    }
}
