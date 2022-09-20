using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using System;

namespace FoltDelivery.Core.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string token);
    }
}
