using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.RestaurantAggregate
{
    public class Location : Entity
    {
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
        public virtual Address Address { get; set; }

        public Location(Guid id):base(id) { }

        public Location(Guid id, Double longitude, Double latitude, Address address) : base(id)
        {
            Longitude = longitude;
            Latitude = latitude;
            Address = address;
        }
    }
}
