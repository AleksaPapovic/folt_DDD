using AutoMapper;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Infrastructure.Queries;
using System;
using System.Collections.Generic;
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

        public async Task<SuggestionDTO> Handle(GetPersonalSuggestionQuery request, CancellationToken cancellationToken)
        {
            SuggestionDTO suggestion = new SuggestionDTO();
            List<Guid> suggestedIds = await _orderRepository.GetSuggestedFromPersonalOrders(request.Order);
            if (suggestedIds != null && suggestedIds.Count != 0)
            {
                foreach (Guid suggestedProductId in suggestedIds)
                {
                    suggestion.SuggestedProducts.Add(_mapper.Map<ProductDTO>(_productRepository.Get(suggestedProductId)));
                }
            }
            return suggestion;
        }
    }
}
