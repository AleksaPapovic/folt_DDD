using FoltDelivery.Core.Domain;
using FoltDelivery.Core.Projections;
using System;
using System.Collections.Generic;


namespace FoltDelivery.Core.Domain.Aggregate
{
    public interface IAggregate : IAggregate<Guid> { }

    public interface IAggregate<out T> : IProjection
    {
        int Version { get; }
        List<DomainEvent> DeleteUncommittedEvents();
    }
}
