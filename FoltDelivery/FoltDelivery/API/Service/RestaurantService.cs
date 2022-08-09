using System;
using System.Collections.Generic;
using AutoMapper;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.API.Repository;

namespace FoltDelivery.API.Service
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IRestaurantRepository _restaurantRepository;
        private IJwtUtils _jwtUtils;
        private IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IJwtUtils jwtUtils, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public Restaurant GetRestaurant(Guid id)
        {
            return _restaurantRepository.Get(id);
        }

    }
}
