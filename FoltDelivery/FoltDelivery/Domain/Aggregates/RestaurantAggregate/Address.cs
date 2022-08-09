using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.RestaurantAggregate
{
    public class Address : Entity
    {
        public String StreetName { get; set; }

        public int HouseNumber { get; set; }

        public String City { get; set; }

        public int PostalCode { get; set; }

        public String State { get; set; }

        public Address(Guid id):base(id) { }

        public Address(String streetName, int houseNumber, String city, int postalCode, String state) : base(Guid.NewGuid())
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
            State = state;
        }
    }
}
