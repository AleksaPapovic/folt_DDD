using System.Threading;
using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure.Events
{
    public interface IEventBus
    {
        Task Publish(IEventEnvelope @event, CancellationToken ct);
    }
}
