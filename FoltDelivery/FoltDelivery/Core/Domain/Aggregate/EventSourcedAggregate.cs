using FoltDelivery.Core.Domain;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Core.Domain.Aggregate
{
    public abstract class EventSourcedAggregate : Entity, IAggregate
    {
        public virtual List<DomainEvent> Changes { get; protected set; }
        public int Version { get; protected set; }
        public int InitialVersion { get; protected set; }
        public virtual List<DomainEvent> UncommittedEvents { get; protected set; }

        public EventSourcedAggregate():base(Guid.NewGuid())
        {
            Changes = new List<DomainEvent>();
            UncommittedEvents = new List<DomainEvent>();
        }

        public EventSourcedAggregate(Guid id):base(id)
        {
            Changes = new List<DomainEvent>();
            UncommittedEvents = new List<DomainEvent>();
        }

        public abstract void When(DomainEvent @event);

        public List<DomainEvent> DeleteUncommittedEvents()
        {
            List<DomainEvent> uncommitedEvents = UncommittedEvents;
            UncommittedEvents.Clear();
            return uncommitedEvents;
        }

        protected void AddUncommittedEvent(DomainEvent @event)
        {
            UncommittedEvents.Add(@event);
        }
    }
}
