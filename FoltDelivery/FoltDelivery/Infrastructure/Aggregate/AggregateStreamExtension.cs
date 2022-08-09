using EventStore.Client;
using FoltDelivery.Infrastructure.Events;
using FoltDelivery.Infrastructure.Exceptions;
using FoltDelivery.Infrastructure.Projections;
using System;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure.Aggregate
{
    public static class AggregateStreamExtensions
    {
        public static async Task<T?> AggregateStream<T>(
            this EventStoreClient eventStore,
            Guid id,
            System.Threading.CancellationToken cancellationToken,
            ulong? fromVersion = null
        ) where T : class, IProjection
        {
            var readResult = eventStore.ReadStreamAsync(
                Direction.Forwards,
                StreamNameMapper.ToStreamId<T>(id),
                fromVersion ?? StreamPosition.Start,
                cancellationToken: cancellationToken
            );

            var readState = await readResult.ReadState;

            if (readState == ReadState.StreamNotFound)
                throw AggregateNotFoundException.For<T>(id);

            var aggregate = (T)Activator.CreateInstance(typeof(T), true)!;

            await foreach (var @event in readResult)
            {
                var eventData = @event.Deserialize();

                aggregate.When(eventData!);
            }

            return aggregate;
        }
    }
}