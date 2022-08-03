using System.Threading;
using System.Threading.Tasks;
using FoltDelivery.Infrastructure.Events;

namespace FoltDelivery.Infrastructure
{
    public interface IEventHandler<in TEvent> where TEvent : IEventEnvelope
    {
        Task HandleAsync(IEventEnvelope @event, CancellationToken cancel);
    }
}
