using AutoMapper;
using FoltDelivery.API.Commands;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Repository;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Core.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.API.Handlers
{
    public class RemoveOrderItemHandler : ICommandHandler<RemoveOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public RemoveOrderItemHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            ProductDTO product = _mapper.Map<ProductDTO>(_productRepository.Get(request.OrderUpdated.OrderItemId));
            request.OrderUpdated.Price = new Money(product.Price.Amount);
            Order order = _orderRepository.FindBy(request.OrderUpdated.Id);
            order.UpdateOrderItems(request.OrderUpdated.OrderItemId, false);
            request.OrderUpdated.OrderItems = order.OrderItems;
            order.RemoveItem(request.OrderUpdated);
            _orderRepository.Save(order);

            return Task.FromResult(Unit.Value);
        }


    }
}
