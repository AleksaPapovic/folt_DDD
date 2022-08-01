using System;
using System.Collections.Generic;
using FoltDelivery.Domain.Aggregates.Product;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.Order
{
    public class ShippingCost : ValueObject<ShippingCost>, IComparable<ShippingCost>
    {
        public decimal Price { get; private set; }

        public ShippingCost()
            : this(0m)
        {
        }

        public ShippingCost(decimal price)
        {
            ThrowExceptionIfNotValid(price);

            Price = price;
        }

        private void ThrowExceptionIfNotValid(decimal price)
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
