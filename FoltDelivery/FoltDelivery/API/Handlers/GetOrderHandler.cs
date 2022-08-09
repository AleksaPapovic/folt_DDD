using System.Threading;
using System.Threading.Tasks;
using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Infrastructure.Queries;

namespace FoltDelivery.API.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrderQuery, OrderAggregate>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<OrderAggregate> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_orderRepository.FindBy(request.OrderId));
        }
    }
}
