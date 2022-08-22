namespace FoltDelivery.Infrastructure.Projections
{
    public interface IProjection
    {
        void When(DomainEvent @event);
    }
}
