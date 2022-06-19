using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;
using FluentAssertions;
using EventData = EventStore.ClientAPI.EventData;
using ResolvedEvent = EventStore.ClientAPI.ResolvedEvent;
using StreamPosition = EventStore.ClientAPI.StreamPosition;

namespace FoltDelivery.Infrastructure
{

 
        public class GetEventStore : IEventStore
        {
            private IEventStoreConnection _esConn;

            private const string EventClrTypeHeader = "EventClrTypeName";

            public GetEventStore(IEventStoreConnection esConn)
            {
            //IEventStoreConnection _esConn = esConn;
            this._esConn = ConnectionFactory.Create();
            this.getConnection();

        }

        public async Task getConnection()
            {
                await this._esConn.ConnectAsync();
            }
            public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
            {
                // ES will automatically create a stream when events are added to it
                AppendEventsToStream(streamName, domainEvents, null);
            }

            public void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion)
            {
                var commitId = Guid.NewGuid();
                var eventsInStorageFormat = domainEvents.Select(e => MapToEventStoreStorageFormat(e, commitId, e.Id));
                _esConn.AppendToStreamAsync(StreamName(streamName), expectedVersion ?? ExpectedVersion.Any, eventsInStorageFormat);
            }

            private EventData MapToEventStoreStorageFormat(object evnt, Guid commitId, Guid eventId)
            {
                var headers = new Dictionary<string, object>
            {
                // each event will be associated with the same commit
                {"CommitId", commitId}, 
                
                // store type of class so event can be rebuilt when the event is loaded
                {EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName}
            };

                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt));
                var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers));
                var isJson = true;

                return new EventData(eventId, evnt.GetType().Name, isJson, data, metadata);
            }

            public IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
            {
                // ES wants the number of events to retrieve not highest version
                var amount = (toVersion - fromVersion) + 1;
                Console.WriteLine("Amount: " + amount);
                var eventsTask= _esConn.ReadStreamEventsForwardAsync(StreamName(streamName), fromVersion, amount, false); // last param not important here
                 //if(eventsTask.IsCompleted){ this.Close(); }
                // map events back from JSON string to DomainEvent. Header indicates the type
                return eventsTask.Result.Events.Select(e => (DomainEvent)RebuildEvent(e));
            }
            public void Close()
            {
                this._esConn.Close();
            }
        private object RebuildEvent(ResolvedEvent eventStoreEvent)
            {
                var metadata = eventStoreEvent.OriginalEvent.Metadata;
                var data = eventStoreEvent.OriginalEvent.Data;
                var typeOfDomainEvent = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
                var rebuiltEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)typeOfDomainEvent));
                return rebuiltEvent;
            }

            // snapshots in Event Store are just events in dedicated snapshot streams
            // explained: http://stackoverflow.com/questions/16359330/is-snapshot-supported-from-greg-young-eventstore
            public void AddSnapshot<T>(string streamName, T snapshot)
            {
                var stream = SnapshotStreamNameFor(streamName);
                var snapshotAsEvent = MapToEventStoreStorageFormat(snapshot, Guid.NewGuid(), Guid.NewGuid());
                _esConn.AppendToStreamAsync(stream, ExpectedVersion.Any, snapshotAsEvent);
            }

            public T GetLatestSnapshot<T>(string streamName) where T : class
            {
                var stream = SnapshotStreamNameFor(streamName);
                var amountToFetch = 1; // just the latest one
                var ev = _esConn.ReadStreamEventsBackwardAsync(stream, StreamPosition.End, amountToFetch, false);
                if (ev.Result.Events.Any())
                    return (T)RebuildEvent(ev.Result.Events.Single());
                else
                    return null;
            }

            private string SnapshotStreamNameFor(string streamName)
            {
                // snapshots are just events in separate streams
                return StreamName(streamName) + "-snapshots";
            }

            private string StreamName(string streamName)
            {
                // Get Event Store projections require only a single hypen ("-")
                // see: https://groups.google.com/forum/#!msg/event-store/D477bKLcdI8/62iFGhHdMMIJ
                var sp = streamName.Split(new[] { '-' }, 2);
                return sp[0] + "-" + sp[1].Replace("-", "");
            }
        }
    }


