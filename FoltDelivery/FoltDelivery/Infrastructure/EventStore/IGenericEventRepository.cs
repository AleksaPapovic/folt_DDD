using System;
using FoltDelivery.Domain.Aggregates.OrderAggregate;

namespace FoltDelivery.Infrastructure.EventStore
{
    public interface IGenericEventRepository<T, S> where T : class where S : class
    {
        public T FindBy(Guid aggregateId);
        public void Add(T aggregate);
        public void Save(T aggregate);
        public void SaveSnapshot(S snapshot, T aggregate);
    }
}
