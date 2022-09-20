using FoltDelivery.Core.Domain;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class ShippingCost : ValueObject<ShippingCost>, IComparable<ShippingCost>
    {
        public Double Price { get; private set; }

        public ShippingCost()
            : this(0)
        {
        }

        public ShippingCost(Double price)
        {
            ThrowExceptionIfNotValid(price);

            Price = price;
        }

        private void ThrowExceptionIfNotValid(Double price)
        {
            if (price < 0)
                throw new ArgumentException("Shipping cost cannot be a negative price");
        }

        public ShippingCost Add(ShippingCost shippingCost)
        {
            return new ShippingCost(this.Price + shippingCost.Price);
        }

        public bool IsGreaterThanOrEqualTo(ShippingCost shippingCost)
        {
            return this.Price >= shippingCost.Price;
        }

        public ShippingCost Subtract(ShippingCost shippingCost)
        {
            return new ShippingCost(this.Price - shippingCost.Price);
        }

        public ShippingCost MultiplyBy(int number)
        {
            return new ShippingCost(this.Price * number);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Price };
        }

        public int CompareTo(ShippingCost other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }
}
