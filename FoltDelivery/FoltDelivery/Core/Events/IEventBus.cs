using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.Core.Events
{
    public interface IEventBus
    {
        Task Publish(IEventEnvelope @event, CancellationToken ct);
    }
}
