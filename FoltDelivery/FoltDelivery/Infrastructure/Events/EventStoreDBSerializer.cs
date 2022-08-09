﻿using System.Text;
using EventStore.Client;
using FoltDelivery.Infrastructure.Events.FoltDelivery.Infrastructure.Events;
using Newtonsoft.Json;

namespace FoltDelivery.Infrastructure.Events
{
    public static class EventStoreDBSerializer
    {
        public static T? Deserialize<T>(this ResolvedEvent resolvedEvent) where T : class =>
            Deserialize(resolvedEvent) as T;

        public static object? Deserialize(this ResolvedEvent resolvedEvent)
        {
            // get type
            var eventType = EventTypeMapper.ToType(resolvedEvent.Event.EventType);

            return eventType != null
                // deserialize event
                ? JsonConvert.DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span), eventType)
                : null;
        }

        public static EventData ToJsonEventData(this object @event) =>
            new(
                Uuid.NewUuid(),
                EventTypeMapper.ToName(@event.GetType()),
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { }))
            );
    }

}
