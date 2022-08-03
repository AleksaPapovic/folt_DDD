using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FoltDelivery.Infrastructure.Tracing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;

namespace FoltDelivery.Infrastructure.Events
{
    public class EventBus : IEventBus 
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Func<IServiceProvider, IEventEnvelope?, TracingScope> createTracingScope;
        private readonly AsyncPolicy retryPolicy;
        private static readonly ConcurrentDictionary<Type, MethodInfo> PublishMethods = new();

        public EventBus(
            IServiceProvider serviceProvider,
            Func<IServiceProvider, IEventEnvelope?, TracingScope> createTracingScope,
            AsyncPolicy retryPolicy
        )
        {
            this.serviceProvider = serviceProvider;
            this.createTracingScope = createTracingScope;
            this.retryPolicy = retryPolicy;
        }

        private async Task Publish<TEvent>(IEventEnvelope @event, CancellationToken ct)
        {
            //var eventEnvelope = @event as IEventEnvelope; //IEventEnvelope
            using var scope = serviceProvider.CreateScope();
            //using var tracingScope = createTracingScope(scope.ServiceProvider, eventEnvelope);

            var eventHandlers =
                scope.ServiceProvider.GetServices<IEventHandler<IEventEnvelope>>();

            foreach (var eventHandler in eventHandlers)
            {
                await retryPolicy.ExecuteAsync(async token =>
                {
                    await eventHandler.HandleAsync(@event, token);
                }, ct);
            }
        }

        public async Task Publish(IEventEnvelope eventEnvelope, CancellationToken ct)
        {
            // publish also just event data
            // thanks to that both handlers with envelope and without will be called
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
        public static IServiceCollection AddEventBus(this IServiceCollection services, AsyncPolicy? asyncPolicy = null)
        {
            services.AddSingleton(sp => new EventBus(
                sp,
                sp.GetRequiredService<ITracingScopeFactory>().CreateTraceScope,
                asyncPolicy ?? Policy.NoOpAsync()
            ));
            services
                .TryAddSingleton<IEventBus>(sp => sp.GetRequiredService<EventBus>());

            return services;
        }
    }
}
