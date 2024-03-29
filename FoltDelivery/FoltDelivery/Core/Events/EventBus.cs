﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;

namespace FoltDelivery.Core.Events
{
    public class EventBus : IEventBus 
    {
        private readonly IServiceProvider serviceProvider;
        private static readonly ConcurrentDictionary<Type, MethodInfo> PublishMethods = new();

        public EventBus(
            IServiceProvider serviceProvider
        )
        {
            this.serviceProvider = serviceProvider;
        }

        private void Publish<TEvent>(IEventEnvelope @event, CancellationToken ct)
        {
            var eventEnvelope = @event as IEventEnvelope;
            using var scope = serviceProvider.CreateScope();

            var eventHandlers =
                scope.ServiceProvider.GetServices<IEventHandler<IEventEnvelope>>();
        }

        public async Task Publish(IEventEnvelope eventEnvelope, CancellationToken ct)
        {
            await (Task)GetGenericPublishFor(eventEnvelope.Data)
                .Invoke(this, new[] { eventEnvelope.Data, ct })!;

            await (Task)GetGenericPublishFor(eventEnvelope)
                .Invoke(this, new object[] { eventEnvelope, ct })!;
        }

        private static MethodInfo GetGenericPublishFor(object @event) =>
            PublishMethods.GetOrAdd(@event.GetType(), eventType =>
                typeof(EventBus)
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Single(m => m.Name == nameof(Publish) && m.GetGenericArguments().Any())
                    .MakeGenericMethod(eventType)
            );
    }

    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, AsyncPolicy asyncPolicy = null)
        {
            services.AddSingleton(sp => new EventBus(sp));
            services.TryAddSingleton<IEventBus>(sp => sp.GetRequiredService<EventBus>());

            return services;
        }
    }
}
