using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FoltDelivery.Domain.Aggregates.Customer;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FoltDeliveryDbContext _dbContext;

        public UserRepository(FoltDeliveryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUsingCredentials(string username, string password)
        {
            return null;
            //return _dbContext.Users.Where(p => p.Username == username && p.Password == password && !p.Blocked).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Username.Equals(username));
        }
    }
}
