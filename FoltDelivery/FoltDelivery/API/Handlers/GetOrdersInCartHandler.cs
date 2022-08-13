using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.API.Handlers
{
    public class GetOrdersInCartHandler : IQueryHandler<GetOrdersInCartQuery, List<OrderAggregate>>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IUserRepository _userRepository;

        public GetOrdersInCartHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public Task<List<OrderAggregate>> Handle(GetOrdersInCartQuery request, CancellationToken cancellationToken)
        {
            User user = _userRepository.Get(request.userId);
            List<OrderAggregate> orders = new List<OrderAggregate>();
            foreach (Guid orderId in user.CustomerOrdersIds)
            {
                orders.Add(_orderRepository.FindBy(orderId));
            }
            return Task.FromResult(orders);

        }

    }
}
