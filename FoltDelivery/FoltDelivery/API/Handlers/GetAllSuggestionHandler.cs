using AutoMapper;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Core.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.API.Handlers
{
    public class GetAllSuggestionHandler : IQueryHandler<GetAllSuggestionQuery, SuggestionDTO>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        public GetAllSuggestionHandler(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        async Task<SuggestionDTO> IRequestHandler<GetAllSuggestionQuery, SuggestionDTO>.Handle(GetAllSuggestionQuery request, CancellationToken cancellationToken)
        {
            SuggestionDTO suggestion = new SuggestionDTO();
            List<Guid> suggestedIds = null;
            suggestedIds = await _orderRepository.GetSuggestedFromAllOrders(request.Order);
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

