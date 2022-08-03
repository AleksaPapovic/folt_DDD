using System;

namespace FoltDelivery.Infrastructure.OptimisticConcurrency
{
    public class NextResourceVersionProvider : IExpectedResourceVersionProvider
    {
        private readonly Func<string>? customGet;
        private string? value;

        public NextResourceVersionProvider(Func<string?>? customGet = null)
        {
            this.customGet = customGet;
        }

        public string? Value
        {
            get => customGet != null ? customGet() : value;
            private set => this.value = value;
        }

        public bool TrySet(string newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
                return false;

            Value = newValue;
            return true;
        }
    }
}
