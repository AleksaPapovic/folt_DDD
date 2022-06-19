using FoltDelivery.Model;
using System.Collections.Generic;

namespace FoltDelivery.Repository
{
    public interface GenericRepository<T> where T : Entity
    {
        public List<T> GetAll();
        public T Get(string id);
        public void SaveAll();
        public void Save(T entity);
        public void Update(T entity);
        public void Delete(string id);
        public string GenerateId();
    }
}
