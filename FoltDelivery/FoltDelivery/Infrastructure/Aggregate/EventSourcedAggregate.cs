using System;
using System.Collections.Generic;
using FoltDelivery.Infrastructure.Projections;
using FoltDelivery.Model;

namespace FoltDelivery.Infrastructure.Aggregate
{
    public abstract class EventSourcedAggregate :IAggregate
    {
        public Guid Id { get; protected set; }
        public List<DomainEvent> Changes { get; protected set; }
        public int Version { get; protected set; }
        public int InitialVersion { get; protected set; }
        public Queue<object> UncommittedEvents { get; protected set; }

        public EventSourcedAggregate() {
            Changes = new List<DomainEvent>();
            UncommittedEvents = new Queue<object>();
        }

        public EventSourcedAggregate(Guid id) 
        {
            Id = id;
            Changes = new List<DomainEvent>();
            UncommittedEvents = new Queue<object>();
        }

        public abstract void Apply(DomainEvent changes);

        public object[] DequeueUncommittedEvents()
        {
            var dequeuedEvents = UncommittedEvents.ToArray();

            UncommittedEvents.Clear();

            return dequeuedEvents;
        }

        protected void Enqueue(object @event)
        {
            UncommittedEvents.Enqueue(@event);
        }

        public void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
