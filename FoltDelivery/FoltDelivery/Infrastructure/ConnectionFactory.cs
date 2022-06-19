using System;
using EventStore.ClientAPI;

namespace FoltDelivery.Infrastructure{
    public static class ConnectionFactory {
        public static IEventStoreConnection Create() => EventStoreConnection.Create(ConnectionSettings.Create().DisableTls().Build(),new Uri("tcp://admin:changeit@localhost:1113"));
    }
}
