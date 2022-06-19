using FoltDelivery.Model;
using FoltDelivery.Repository;
using System.Collections.Generic;

namespace FoltDelivery.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return new List<User>(_userRepository.GetAll());
        }

        public User GetUsingCredentials(string username, string password)
        {
            return _userRepository.GetUsingCredentials(username, password);
        }
    }
}
