using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.Core.Persistance;
using FoltDelivery.Infrastructure.Persistance;

namespace FoltDelivery.API.Repository
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
