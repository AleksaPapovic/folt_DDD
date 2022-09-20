using FoltDelivery.Core.Domain;

namespace FoltDelivery.Core.Projections
{
    public interface IProjection
    {
        void When(DomainEvent @event);
    }
}
