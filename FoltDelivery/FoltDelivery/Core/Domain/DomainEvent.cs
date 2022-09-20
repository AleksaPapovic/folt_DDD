using System;

namespace FoltDelivery.Core.Domain
{
    public class DomainEvent : Entity
    {
        public string EventType { get; protected set; }

        public int Version { get; protected set; }

        public DomainEvent() : base(Guid.NewGuid()) { }
        public DomainEvent(Guid aggregateId, string eventType) : base(aggregateId)
        {
            EventType = eventType;
        }

    }
}
