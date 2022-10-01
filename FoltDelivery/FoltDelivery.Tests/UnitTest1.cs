using EventStore.ClientAPI;
using FluentAssertions;
using FoltDelivery.Infrastructure.EventStore;
using System.Text;


namespace FoltDelivery.Tests
{


    public class EventStoreTest
    {

        [Fact]
        public async void AppendEventToEventStore_ShouldSucceed()
        {
            var connection = ConnectionFactory.Create();
            await connection.ConnectAsync();

            var streamName = "order-test";

            var events = new object[] { };
            var appendedEvents = 0l;

            const string eventType = "event-type";
            const string data = "{ \"test\":\"test\"}";
            const string metadata = "{}";

            var eventPayload = new EventData(
                eventId: Guid.NewGuid(),
                type: eventType,
                isJson: true,
                data: Encoding.UTF8.GetBytes(data),
                metadata: Encoding.UTF8.GetBytes(metadata)
            );


            var result = await connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventPayload);

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventPayload);
                appendedEvents = result.NextExpectedVersion;
            });

            exception.Should().BeNull();
        }
    }
}
