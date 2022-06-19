using FoltDelivery.Model;

namespace FoltDelivery.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetUsingCredentials(string username, string password);
    }
}
