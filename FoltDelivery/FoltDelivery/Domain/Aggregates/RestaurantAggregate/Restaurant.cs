using System;
using System.Collections.Generic;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Aggregate;

namespace FoltDelivery.Domain.Aggregates.RestaurantAggregate
{
    public class Restaurant : EventSourcedAggregate
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public virtual RestaurantMenu Menu { get; set; }
        public RestaurantStatus Status { get; set; }
        public virtual Location Location { get; set; }
        public int LogoId { get; set; }
        public Boolean Deleted { get; set; }

        public Restaurant() : base(Guid.NewGuid()) { }
        public Restaurant(Guid id) : base(id)
        {
        }

        public Restaurant(Guid id, String name, String type, List<int> articlesIds, RestaurantStatus status,
            Location location, int logoId, Boolean deleted) : base(id)
        {
            Name = name;
            Type = type;
            Status = status;
            Location = location;
            LogoId = logoId;
            Deleted = deleted;
        }


        public Restaurant(Restaurant restaurant) : base(restaurant.Id)
        {
            Name = restaurant.Name;
            Type = restaurant.Type;
            Status = restaurant.Status;
            Location = restaurant.Location;
            LogoId = restaurant.LogoId;
            Deleted = restaurant.Deleted;
        }

        public override void Apply(DomainEvent changes)
        {
            throw new NotImplementedException();
        }
    }
}
