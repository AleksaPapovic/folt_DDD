using System;
using System.Collections.Generic;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.ProductAggregate
{
    public class Money : ValueObject<Money>, IComparable<Money>
    {
        public Double Amount { get; protected set; }

        public Money()
            : this(0)
        {
        }

        public Money(Double amount)
        {
            ThrowExceptionIfNotValid(amount);

            Amount = amount;
        }

        private void ThrowExceptionIfNotValid(Double amount)
        {

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
