using System;
using System.Collections.Generic;
using AutoMapper;
using EventStore.ClientAPI;
using FoltDelivery.Domain.Aggregates.Order;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.Repository;

namespace FoltDelivery.Service
{
    public class OrderService:IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private IEventStore _eventStore;
        private IJwtUtils _jwtUtils;
        private IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IEventStore eventStore, IJwtUtils jwtUtils, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _eventStore = eventStore;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public Order CreateOrder(Order newOrder)
        {
            List<OrderCreated> events = new List<OrderCreated>();
            OrderCreated evt = new OrderCreated(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"));
            _eventStore.CreateNewStream("test-0", events);
            
            return newOrder;
        }
    }
}
