using FoltDelivery.Core.Domain.Aggregate;
using FoltDelivery.Core.Domain;
using FoltDelivery.Core.Enums;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Domain.Aggregates.CustomerAggregate
{
    public class User : EventSourcedAggregate
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public Gender Gender { get; set; }
        public String DateOfBirth { get; set; }
        public virtual Role Role { get; set; }
        public List<Guid> CustomerOrdersIds { get; set; }
        public int CartId { get; set; }
        public int RestaurantId { get; set; }
        public List<Guid> DeliveryOrdersIds { get; set; }
        public double Points { get; set; }
        public virtual Loyalty Type { get; set; }
        public bool LogicalDeleted { get; set; }
        public bool Blocked { get; set; }
        public bool Confirmed { get; set; }

        public User(Guid id) : base(id) { }

        public User(Guid id, String username, String password, String name, String surname, Gender gender,
            String dateOfBirth,
            Role role, List<Guid> customerOrdersIds, int cartId, int restaurantId,
            List<Guid> deliveryOrdersIds, double points, Loyalty type, bool logicalDeleted,
            bool blocked) : base(id)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Role = role;
            CustomerOrdersIds = customerOrdersIds;
            CartId = cartId;
            RestaurantId = restaurantId;
            DeliveryOrdersIds = deliveryOrdersIds;
            Points = points;
            Type = type;
            LogicalDeleted = logicalDeleted;
            Blocked = blocked;
        }

        public User(User user) : base(user.Id)
        {
            Username = user.Username;
            Password = user.Password;
            Name = user.Name;
            Surname = user.Surname;
            Gender = user.Gender;
            DateOfBirth = user.DateOfBirth;
            Role = user.Role;
            CustomerOrdersIds = user.CustomerOrdersIds;
            CartId = user.CartId;
            RestaurantId = user.RestaurantId;
            DeliveryOrdersIds = user.DeliveryOrdersIds;
            Points = user.Points;
            Type = user.Type;
            LogicalDeleted = user.LogicalDeleted;
            Blocked = user.Blocked;
        }

        public override void When(DomainEvent changes)
        {
            throw new NotImplementedException();
        }
    }
}
