using System.Threading;
using System.Threading.Tasks;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.Order;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Queries;

namespace FoltDelivery.API.Queries
{
    public class GetOrderHandler:IQueryHandler<GetOrderQuery,Order>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_orderRepository.FindBy(request.OrderId));
        }
    }
}
