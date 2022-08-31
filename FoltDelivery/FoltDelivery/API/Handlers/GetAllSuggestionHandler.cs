using AutoMapper;
using FoltDelivery.API.DTO;
using FoltDelivery.API.Queries;
using FoltDelivery.API.Repository;
using FoltDelivery.Infrastructure.Queries;
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

        Task<SuggestionDTO> IRequestHandler<GetAllSuggestionQuery, SuggestionDTO>.Handle(GetAllSuggestionQuery request, CancellationToken cancellationToken)
        {
          SuggestionDTO suggestion = new SuggestionDTO();
          List<Guid> suggestedIds = _orderRepository.GetSuggestedFromAllOrders(request.Order).Result;
          suggestion.SuggestedProducts = _mapper.Map<List<ProductDTO>>(_productRepository.GetAll().Where(p => suggestedIds.All(p2 => 
          p2 == p.Id)));
          return Task.FromResult(suggestion);
        }
    }
}
