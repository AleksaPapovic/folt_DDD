using FoltDelivery.Model;
using System.Collections.Generic;

namespace FoltDelivery.Service
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUsingCredentials(string username, string password);
    }
}
