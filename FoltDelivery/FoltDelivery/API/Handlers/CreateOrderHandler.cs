using AutoMapper;
using FoltDelivery.API.Commands;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Infrastructure.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.API.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public CreateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            foreach (var article in request.order.OrderQuantities)
            {
                MoneyDTO productPrice = _mapper.Map<MoneyDTO>(_productRepository.GetPrice(article.Key));
                OrderItem orderItem = new OrderItem(article.Key, article.Value, _mapper.Map<Money>(productPrice));
                request.order.OrderItems.Add(article.Key, orderItem);
            }

            OrderAggregate order = new OrderAggregate(request.order);
            _orderRepository.Add(order);

            User currentUser = _userRepository.Get(request.order.CustomerId);
            currentUser.CustomerOrdersIds.Add(request.order.Id);
            _userRepository.Update(currentUser);

            return null;
        }
    }
}
