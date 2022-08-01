using System;
using FoltDelivery.Domain.Aggregates.Customer;

namespace FoltDelivery.Infrastructure.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string token);
    }
}
