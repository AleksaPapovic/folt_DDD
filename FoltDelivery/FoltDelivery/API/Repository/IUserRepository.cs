using FoltDelivery.Domain.Aggregates.CustomerAggregate;

namespace FoltDelivery.API.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetUsingCredentials(string username, string password);
        public User GetByUsername(string username);
    }
}
