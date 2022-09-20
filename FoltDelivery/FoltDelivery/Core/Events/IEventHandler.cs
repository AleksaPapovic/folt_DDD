using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.Core.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEventEnvelope
    {
        Task HandleAsync(IEventEnvelope @event, CancellationToken cancel);
    }
}
