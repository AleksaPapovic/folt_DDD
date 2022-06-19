using System;
using System.Collections.Generic;
using FoltDelivery.Model;

namespace FoltDelivery.Infrastructure
{
    public abstract class EventSourcedAggregate : Entity
    {
        public List<DomainEvent> Changes { get; private set; }
        public int Version { get; protected set; }

        public EventSourcedAggregate(Guid id)
        {
            Id = id;
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);
    }
}
