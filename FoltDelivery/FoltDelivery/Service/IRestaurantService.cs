using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.Restaurant;

namespace FoltDelivery.Service
{
    public interface IRestaurantService
    {
        public List<Restaurant> GetAllRestaurants();
        public Restaurant GetRestaurant(Guid id);
    }
}
