using System.Threading.Tasks;

namespace FoltDelivery.Infrastructure
{
    public interface IEventHandler<TEvent>
        where TEvent : DomainEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
