using AutoMapper;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.API.DTO;

namespace Agents.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<OrderDTO, OrderAggregate>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<RestaurantDTO, Restaurant>().ReverseMap();
        }
    }
}
