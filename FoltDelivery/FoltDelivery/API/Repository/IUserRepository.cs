using FoltDelivery.Domain.Aggregates.CustomerAggregate;

namespace FoltDelivery.API.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
    }
}
