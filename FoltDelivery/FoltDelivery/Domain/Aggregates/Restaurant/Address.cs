using System;

namespace FoltDelivery.Model
{
    public class Address : Entity
    {
        public String StreetName { get; set; }

        public int HouseNumber { get; set; }

        public String City { get; set; }

        public int PostalCode { get; set; }

        public String State { get; set; }

        public Address() { }

        public Address(String streetName, int houseNumber, String city, int postalCode, String state)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
            State = state;
        }
    }
}
