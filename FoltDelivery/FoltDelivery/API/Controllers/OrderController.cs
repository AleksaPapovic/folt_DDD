using AutoMapper;
using FoltDelivery.API.Commands;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Queries;
using FoltDelivery.API.Service;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Commands;
using FoltDelivery.Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoltDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        private readonly IQueryBus _queryBus;

        private IOrderService _orderService;

        private IUserService _userService;

        private IMapper _mapper;

        public OrderController(ICommandBus commandBus,
        IQueryBus queryBus, IOrderService orderService, IUserService userService,IMapper mapper)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _userService = userService;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<OrderAggregate> GetOrder(GetOrderDTO order)
        {
            return await _queryBus.Send<GetOrderQuery, OrderAggregate>(GetOrderQuery.Create(order.Id));
        }

        [HttpPost]
        public  void CreateOrder([FromBody] OrderDTO newOrder)
        {
             _commandBus.Send(CreateOrderCommand.Create(newOrder));
        }

    }
}