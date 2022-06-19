using System;

namespace FoltDelivery.Model
{
    public class Location : Entity
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public Address Address { get; set; }

        public Location() { }

        public Location(Guid id, float longitude, float latitude, Address address)
        {
            Id = id;
            Longitude = longitude;
            Latitude = latitude;
            Address = address;
        }
    }
}
