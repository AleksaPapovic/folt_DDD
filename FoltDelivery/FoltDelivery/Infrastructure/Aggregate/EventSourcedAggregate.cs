using System;
using System.Collections.Generic;
using FoltDelivery.Infrastructure.Projections;
using FoltDelivery.Model;

namespace FoltDelivery.Infrastructure.Aggregate
{
    public abstract class EventSourcedAggregate :IAggregate
    {
        public Guid Id { get; protected set; }
        public virtual List<DomainEvent> Changes { get; protected set; }
        public int Version { get; protected set; }
        public int InitialVersion { get; protected set; }
        public virtual List<DomainEvent> UncommittedEvents { get; protected set; }

        public EventSourcedAggregate() {
            Changes = new List<DomainEvent>();
            UncommittedEvents = new List<DomainEvent>();
        }

        public EventSourcedAggregate(Guid id) 
        {
            Id = id;
            Changes = new List<DomainEvent>();
            UncommittedEvents = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);

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

        public void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
