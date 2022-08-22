using AutoMapper;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Domain.Aggregates.OrderAggregate;
using FoltDelivery.Domain.Aggregates.ProductAggregate;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.API.DTO;
using System.Collections.Generic;
using System;

namespace FoltDelivery.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<OrderDTO, OrderAggregate>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<RestaurantDTO, Restaurant>().ReverseMap();
            CreateMap<MoneyDTO, Money>().ReverseMap();
        }

        public static Dictionary<Guid, OrderItem> ConvertToOrderItemMap(Dictionary<Guid, OrderItemDTO> orderItemsDTO)
        {
            Dictionary<Guid, OrderItem> orderItems = new Dictionary<Guid, OrderItem>();
            foreach (var orderItemDTO in orderItemsDTO)
            {
                orderItems.Add(orderItemDTO.Key, new OrderItem(orderItemDTO.Value));
            }
            return orderItems;
        }

        public static Dictionary<Guid, OrderItemDTO> ConvertToOrderItemDTOMap(Dictionary<Guid, OrderItem> orderItems)
        {
            Dictionary<Guid, OrderItemDTO> orderItemsDTO = new Dictionary<Guid, OrderItemDTO>();
            foreach (var orderItem in orderItems)
            {
                orderItemsDTO.Add(orderItem.Key, new OrderItemDTO(orderItem.Value));
            }
            return orderItemsDTO;
        }

    }
}
