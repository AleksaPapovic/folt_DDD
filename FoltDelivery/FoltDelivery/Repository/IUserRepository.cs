using FoltDelivery.Domain.Aggregates.Customer;

namespace FoltDelivery.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetUsingCredentials(string username, string password);
        public User GetByUsername(string username);
    }
}
