using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.Customer;
using FoltDelivery.DTO;


namespace FoltDelivery.Service
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUsingCredentials(string username, string password);

        public User GetById(Guid id);
        public User Register(UserDTO userDTO);
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model);
    }
}
