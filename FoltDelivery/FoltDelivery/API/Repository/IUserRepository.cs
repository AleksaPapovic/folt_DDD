using FoltDelivery.Domain.Aggregates.Customer;

namespace FoltDelivery.API.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetUsingCredentials(string username, string password);
        public User GetByUsername(string username);
    }
}
