using FoltDelivery.Domain.Aggregates.RestaurantAggregate;
using FoltDelivery.Infrastructure.Persistance;

namespace FoltDelivery.API.Repository
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
    }
}
