using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure
{
    public abstract class DomainEvent
    {


        public DomainEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }

        public Guid Id { get; private set; }

        public string EventType { get; private set; }
    }
}
