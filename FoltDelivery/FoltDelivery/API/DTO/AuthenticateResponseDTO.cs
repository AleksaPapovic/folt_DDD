using System;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Core.Enums;

namespace FoltDelivery.API.DTO
{
    public class AuthenticateResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseDTO(User user, string token)
        {
            Id = user.Id;
            FirstName = user.Name;
            LastName = user.Surname;
            Username = user.Username;
            Role = user.Role;
            Token = token;
        }
    }
}
