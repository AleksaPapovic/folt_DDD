﻿using EventStore.Client;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventData = EventStore.ClientAPI.EventData;
using ResolvedEvent = EventStore.ClientAPI.ResolvedEvent;
using StreamPosition = EventStore.ClientAPI.StreamPosition;


namespace FoltDelivery.Infrastructure
{


    public class EventStore : IEventStore
    {
        private IEventStoreConnection _esConn;
        private readonly EventStoreClient _eventStore;


        private const string EventClrTypeHeader = "EventClrTypeName";

        public EventStore(IEventStoreConnection esConn, EventStoreClient eventStore)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
            _esConn = ConnectionFactory.Create();
            GetConnection();

        }

        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
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
                {"CommitId", commitId},

                {EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName}
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt));
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers));
            var isJson = true;

            return new EventData(eventId, evnt.GetType().Name, isJson, data, metadata);
        }

        public async Task<List<DomainEvent>> GetStream(string streamName, long fromVersion, int toVersion)
        {
            List<DomainEvent> domainEvents = new List<DomainEvent>();
            var readEvents = await _esConn.ReadStreamEventsForwardAsync(StreamName(streamName), 0, 10, true);

            foreach (var evt in readEvents.Events)
            {
               domainEvents.Add(RebuildEvent1(evt));
            }
            return domainEvents;
        }   

        private DomainEvent RebuildEvent1(ResolvedEvent eventStoreEvent)
        {
            var metadata = eventStoreEvent.OriginalEvent.Metadata;
            var data = eventStoreEvent.OriginalEvent.Data;
            var typeOfDomainEvent = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            DomainEvent rebuiltEvent = (DomainEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)typeOfDomainEvent));
            return rebuiltEvent;
        }


        private object RebuildSnapshot(ResolvedEvent eventStoreEvent)
        {
            var metadata = eventStoreEvent.OriginalEvent.Metadata;
            var data = eventStoreEvent.OriginalEvent.Data;
            var typeOfDomainEvent = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            var rebuiltEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)typeOfDomainEvent));
            return rebuiltEvent;
        }

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
            var amountToFetch = 1;
            var ev = _esConn.ReadStreamEventsBackwardAsync(stream, StreamPosition.End, amountToFetch, false);
            return ev.Result.Events.Any() ? (T)RebuildSnapshot(ev.Result.Events.Single()) : null;
        }

        private string SnapshotStreamNameFor(string streamName)
        {
            return StreamName(streamName) + "-snapshots";
        }

        private string StreamName(string streamName)
        {
            // see: https://groups.google.com/forum/#!msg/event-store/D477bKLcdI8/62iFGhHdMMIJ
            var sp = streamName.Split(new[] { '-' }, 2);
            return sp[0] + "-" + sp[1].Replace("-", "");
        }

        private async Task GetConnection()
        {
            await _esConn.ConnectAsync();
        }

        private void CloseConnection()
        {
            _esConn.Close();
        }
    }
}


