using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure
{
    public interface IEventStore
    {
        void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents);
        void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion);
        Task<List<DomainEvent>> GetStream(string streamName, long fromVersion, int toVersion);
        void AddSnapshot<T>(string streamName, T snapshot);
        T GetLatestSnapshot<T>(string streamName) where T : class;
        void CreateProjectionStream(string streamName,string query);
        Task<string> RunProjection(string streamName);

    }
}
