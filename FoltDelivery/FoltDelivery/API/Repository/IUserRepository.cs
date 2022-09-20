using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Infrastructure.Persistance;

namespace FoltDelivery.API.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
    }
}
