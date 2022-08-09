using System;
using FoltDelivery.Infrastructure.Projections;


namespace FoltDelivery.Infrastructure.Aggregate
{
    public interface IAggregate : IAggregate<Guid>
    {
    }

    public interface IAggregate<out T> : IProjection
    {
        T Id { get; }
        int Version { get; }

        object[] DequeueUncommittedEvents();
    }
}
