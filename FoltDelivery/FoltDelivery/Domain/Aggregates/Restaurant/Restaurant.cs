using FoltDelivery.Model.Enums;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Model
{
    public class Restaurant : Entity
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public RestaurantMenu Menu { get; set; }
        public RestaurantStatus Status { get; set; }
        public Location Location { get; set; }
        public int LogoId { get; set; }
        public int LogicalDeleted { get; set; }

        public Restaurant() { }

        public Restaurant(Guid id, String name, String type, List<int> articlesIds, RestaurantStatus status,
            Location location, int logoId, int logicalDeleted)
        {
            Id = id;
            Name = name;
            Type = type;
           // Menu = articlesIds;
            Status = status;
            Location = location;
            LogoId = logoId;
            LogicalDeleted = logicalDeleted;
        }

        public Restaurant(Restaurant restaurant)
        {
            Id = restaurant.Id;
            Name = restaurant.Name;
            Type = restaurant.Type;
           // ArticlesIds = restaurant.ArticlesIds;
            Status = restaurant.Status;
            Location = restaurant.Location;
            LogoId = restaurant.LogoId;
            LogicalDeleted = restaurant.LogicalDeleted;
        }

    }
}
