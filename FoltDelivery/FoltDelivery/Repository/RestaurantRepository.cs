﻿using FoltDelivery.Domain.Aggregates.Restaurant;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Repository
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly FoltDeliveryDbContext _dbContext;
        public RestaurantRepository(FoltDeliveryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
