using System;
using System.Collections.Generic;
using FoltDelivery.Infrastructure;
using FoltDelivery.Model.Enums;
using FoltDelivery.Domain.Aggregates.Customer;

namespace FoltDelivery.DTO
{
    public class UserDTO:Entity
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public Gender Gender { get; set; }
        public String DateOfBirth { get; set; }
        public virtual Role Role { get; set; }

        public List<int> CustomerOrdersIds;
        public int CartId { get; set; }
        public int RestaurantId { get; set; }
        public List<int> DeliveryOrdersIds;
        public double Points { get; set; }
        public virtual Loyalty Type { get; set; }
        public int LogicalDeleted { get; set; }
        public int Commented { get; set; }

        public bool Blocked { get; set; }

        public UserDTO(Guid id) : base(id) { }
    }
}
