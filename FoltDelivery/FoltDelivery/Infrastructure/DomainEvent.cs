using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure
{
    public abstract class DomainEvent
    {
        public Guid Id { get; private set; }
        public string EventType { get; private set; }
        
        public DomainEvent(Guid aggregateId,string eventType)
        {
            Id = aggregateId;
            EventType = eventType;
        }
    }
}
