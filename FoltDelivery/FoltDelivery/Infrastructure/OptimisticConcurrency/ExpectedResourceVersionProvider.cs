using System;

namespace FoltDelivery.Infrastructure.OptimisticConcurrency
{
    public class ExpectedResourceVersionProvider : IExpectedResourceVersionProvider
    {
        private readonly Func<string, bool>? customTrySet;

        public ExpectedResourceVersionProvider(Func<string, bool>? customTrySet = null)
        {
            this.customTrySet = customTrySet;
        }

        public string? Value { get; private set; }

        public bool TrySet(string value)
        {
            if (customTrySet != null)
                return customTrySet(value);

            if (string.IsNullOrWhiteSpace(value))
                return false;

            Value = value;
            return true;
        }
    }
}
