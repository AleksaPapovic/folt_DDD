using System;
using System.Collections.Generic;
using System.Linq;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Aggregate;
using FoltDelivery.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FoltDelivery.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where  T : EventSourcedAggregate
    {
        private readonly FoltDeliveryDbContext _dbContext;
        private readonly DbSet<T> _table;

        public GenericRepository(FoltDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public T Get(Guid id)
        {
            return _table.Find(id);
        }

        public T Insert(T entity)
        {
            var addedEntity = _table.Add(entity);
            Save();
            return addedEntity.Entity;
        }

        public T Update(T entity)
        {
            var updatedEntity = _table.Update(entity);
            Save();
            return updatedEntity.Entity;
        }

        public void Delete(long id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
