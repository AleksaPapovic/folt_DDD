using FoltDelivery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoltDelivery.Repository
{
    public class UserRepository : IUserRepository, IGenericRepository<User>
    {
        private readonly FoltDeliveryDbContext _dbContext;

        public UserRepository(FoltDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUsingCredentials(string username, string password)
        {
            return null;
            //return _dbContext.Users.Where(p => p.Username == username && p.Password == password && !p.Blocked).FirstOrDefault();
        }

        public IList<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public IEnumerable<User> Search(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Save(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
