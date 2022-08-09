using EventStore.ClientAPI;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Service;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.Domain.Events;
using FoltDelivery.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoltDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        private IUserService _userService;

        private IEventStore _eventStore;

        public OrderController(IOrderService orderService, IUserService userService, IEventStore eventStore)
        {
            _userService = userService;
            _eventStore = eventStore;
            _orderService = orderService;
        }

        public static int i = 0;

        [HttpGet]
        public OrderAggregate GetOrder(GetOrderDTO order)
        {
            return _orderService.GetOrder(order.Id);
        }

        [HttpPost]
        [Route("test")]
        public OrderDTO CreateOrder( [FromBody] OrderDTO  newOrder)
        {
            return _orderService.CreateOrder(newOrder);
 
        }

    }
}