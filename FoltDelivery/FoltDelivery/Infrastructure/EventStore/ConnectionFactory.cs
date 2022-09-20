using EventStore.ClientAPI;
using System;

namespace FoltDelivery.Infrastructure.EventStore
{
    public static class ConnectionFactory
    {
        public static IEventStoreConnection Create() => EventStoreConnection.Create(
        ConnectionSettings.Create().DisableTls().Build(), new Uri("tcp://admin:changeit@localhost:1113"));
    }
}
