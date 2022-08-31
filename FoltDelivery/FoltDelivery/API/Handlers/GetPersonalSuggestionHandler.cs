using AutoMapper;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.API.Handlers
{
    public class GetPersonalSuggestionHandler : IQueryHandler<GetPersonalSuggestionQuery, SuggestionDTO>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        public GetPersonalSuggestionHandler(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<SuggestionDTO> Handle(GetPersonalSuggestionQuery request, CancellationToken cancellationToken)
        {
            SuggestionDTO suggestion = new SuggestionDTO();
            List<Guid> suggestedIds = _orderRepository.GetSuggestedFromAllOrders(request.Order).Result;
            suggestion.SuggestedProducts = _mapper.Map<List<ProductDTO>>(_productRepository.GetAll().Where(p => suggestedIds.All(p2 =>
            p2 == p.Id)));
            return Task.FromResult(suggestion);
        }
    }
}
