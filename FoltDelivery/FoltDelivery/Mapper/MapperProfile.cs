using AutoMapper;
using FoltDelivery.Domain.Aggregates.Customer;
using FoltDelivery.Domain.Aggregates.Order;
using FoltDelivery.Domain.Aggregates.Product;
using FoltDelivery.Domain.Aggregates.Restaurant;
using FoltDelivery.DTO;

namespace Agents.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<ArticleDTO, Article>().ReverseMap();
            CreateMap<RestaurantDTO, Restaurant>().ReverseMap();
        }
    }
}
