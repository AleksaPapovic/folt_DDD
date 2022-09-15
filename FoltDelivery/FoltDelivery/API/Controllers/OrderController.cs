using AutoMapper;
using FoltDelivery.API.Commands;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Queries;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.Infrastructure.Commands;
using FoltDelivery.Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IJwtUtils _iJwtUtils;

        private readonly IMapper _mapper;

        public OrderController(ICommandBus commandBus, IQueryBus queryBus, IJwtUtils iJwtUtils, IMapper mapper)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _iJwtUtils = iJwtUtils;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<OrderAggregate> GetOrder(GetOrderDTO order)
        {
            return await _queryBus.Send<GetOrderQuery, OrderAggregate>(GetOrderQuery.Create(order.Id));
        }

        [HttpGet]
        [Route("inCart")]
        public async Task<List<OrderDTO>> GetOrdersInUserCart()
        {
            Guid? userId = GetPrincipalId();
            if (userId != null)
            {
                return _mapper.Map<List<OrderDTO>>(await _queryBus.Send<GetOrdersInCartQuery, List<OrderAggregate>>(GetOrdersInCartQuery.Create(userId.Value)));
            }
            //error
            return null;

        }

        [HttpPost]
        [Route("suggestion/all")]
        public async Task<SuggestionDTO> GetProjections([FromBody] OrderInCartDTO orderInCart)
        {
            Guid? userId = GetPrincipalId();
            if (userId != null)
            {
                orderInCart.OwnerId = userId.Value;
                return await _queryBus.Send<GetAllSuggestionQuery, SuggestionDTO>(GetAllSuggestionQuery.Create(orderInCart));
            }
            //error
            return null;
        }

        [HttpPost]
        [Route("suggestion/personal")]
        public async Task<SuggestionDTO> GetPersonalSuggestions([FromBody] OrderInCartDTO orderInCart)
        {
            Guid? userId = GetPrincipalId();
            if (userId != null)
            {
                orderInCart.OwnerId = userId.Value;
                return await _queryBus.Send<GetPersonalSuggestionQuery, SuggestionDTO>(GetPersonalSuggestionQuery.Create(orderInCart));
            }
            //error
            return null;
        }

        [HttpPost]
        public void CreateOrder([FromBody] OrderDTO newOrder)
        {
            Guid? userId = GetPrincipalId();
            if (userId != null)
            {
                newOrder.CustomerId = userId.Value;
                _commandBus.Send(CreateOrderCommand.Create(newOrder));
            }
            //error
        }

        [HttpPost]
        [Route("add")]
        public void AddOrderItem([FromBody] OrderUpdateDTO newItem)
        {
            Guid? userId = GetPrincipalId();
            if (userId != null)
            {
                newItem.CustomerId = userId.Value;
                _commandBus.Send(AddOrderItemCommand.Create(newItem));
            }
            //error
        }


        [HttpPost]
        [Route("remove")]
        public void RemoveOrderItem([FromBody] OrderUpdateDTO removedItem)
        {
            Guid? userId = GetPrincipalId();
            if (userId != null)
            {
                removedItem.CustomerId = userId.Value;
                _commandBus.Send(RemoveOrderItemCommand.Create(removedItem));
            }
            //error
        }

        private Guid? GetPrincipalId()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Guid? userId = _iJwtUtils.ValidateJwtToken(token);
            return userId;
        }

    }
}