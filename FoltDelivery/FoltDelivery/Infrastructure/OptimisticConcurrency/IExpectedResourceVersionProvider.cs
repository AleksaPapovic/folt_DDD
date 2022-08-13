namespace FoltDelivery.Infrastructure.OptimisticConcurrency
{
    public interface IExpectedResourceVersionProvider
    {
        string Value { get; }
        bool TrySet(string value);
    }
}
