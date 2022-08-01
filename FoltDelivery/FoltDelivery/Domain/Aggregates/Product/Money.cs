using System;
using System.Collections.Generic;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.Product
{
    public class Money : ValueObject<Money>, IComparable<Money>
    {
        public decimal Amount { get; private set; }

        public Money()
            : this(0m)
        {
        }

        public Money(decimal amount)
        {
            ThrowExceptionIfNotValid(amount);

            Amount = amount;
        }

        private void ThrowExceptionIfNotValid(decimal amount)
        {
            if (amount % 0.01m != 0)
                throw new ArgumentException("Amount can be 2 decmial places only.");

            if (amount < 0)
                throw new ArgumentException("Money cannot be a negative amount");
        }

        public Money Add(Money money)
        {
            return new Money(this.Amount + money.Amount);
        }

        public bool IsGreaterThanOrEqualTo(Money money)
        {
            return this.Amount >= money.Amount;
        }

        public Money Subtract(Money money)
        {
            return new Money(this.Amount - money.Amount);
        }

        public Money MultiplyBy(int number)
        {
            return new Money(this.Amount * number);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Amount };
        }

        public int CompareTo(Money other)
        {
            return this.Amount.CompareTo(other.Amount);
        }
    }
}
