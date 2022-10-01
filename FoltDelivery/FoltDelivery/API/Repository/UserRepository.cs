using System;
using System.Linq;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.Core.Persistance;
using FoltDelivery.Infrastructure.Persistance;

namespace FoltDelivery.API.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FoltDeliveryDbContext _dbContext;

        public UserRepository(FoltDeliveryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Username.Equals(username));
        }
    }
}
