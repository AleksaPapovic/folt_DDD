using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Core.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.API.Handlers
{
    public class GetOrdersInCartHandler : IQueryHandler<GetOrdersInCartQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IUserRepository _userRepository;

        public GetOrdersInCartHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public Task<List<Order>> Handle(GetOrdersInCartQuery request, CancellationToken cancellationToken)
        {
            User user = _userRepository.Get(request.userId);
            List<Order> orders = new List<Order>();
            if (user == null) { return null; }
            foreach (Guid orderId in user.CustomerOrdersIds)
            {
                orders.Add(_orderRepository.FindBy(orderId));
            }
            return Task.FromResult(orders);

        }

    }
}
