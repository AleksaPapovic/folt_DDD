namespace FoltDelivery.Infrastructure.Projections
{
    public interface IVersionedProjection : IProjection
    {
        public ulong LastProcessedPosition { get; set; }
    }
}
