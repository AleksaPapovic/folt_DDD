using System;
using System.Collections.Generic;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.OrderAggregate
{
    public class Address : ValueObject<Address>
    {
        public String Street { get; private set; }
        public String City { get; private set; }
        public String State { get; private set; }
        public String Country { get; private set; }
        public String ZipCode { get; private set; }

        public Address() { }

        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public Address(Address address)
        {
            Street = address.Street;
            City = address.City;
            State = address.State;
            Country = address.Country;
            ZipCode = address.ZipCode;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }

    }
}
