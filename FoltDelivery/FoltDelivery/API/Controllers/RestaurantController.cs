using System;
using System.Collections.Generic;
using AutoMapper;
using FoltDelivery.Core.Authorization;
using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;

namespace FoltDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _restaurantService;

        private readonly IConfiguration _configuration;

        private readonly IJwtUtils _iJwtUtils;

        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IConfiguration configuration, IMapper mapper, IJwtUtils iJwtUtils)
        {
            _restaurantService = restaurantService;
            _configuration = configuration;
            _mapper = mapper;
            _iJwtUtils = iJwtUtils;
        }

        [HttpGet]
        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantService.GetAllRestaurants();

        }

        [HttpGet]
        [Route("{id}")]
        public Restaurant GetRestaurant(Guid id)
        {
            return _restaurantService.GetRestaurant(id);

        }
    }
}
