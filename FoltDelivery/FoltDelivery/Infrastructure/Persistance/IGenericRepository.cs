using System;
using System.Collections.Generic;

namespace FoltDelivery.Infrastructure.Persistance
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(Guid id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(long id);
        void Save();
    }
}
