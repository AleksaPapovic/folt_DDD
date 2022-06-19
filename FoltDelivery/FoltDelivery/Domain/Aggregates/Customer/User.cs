using FoltDelivery.Model.Enums;
using System;
using System.Collections.Generic;

namespace FoltDelivery.Model
{
    public class User : Entity
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public Gender Gender { get; set; }
        public String DateOfBirth { get; set; }
        public virtual UserRole Role { get; set; }

        public List<int> CustomerOrdersIds;
        public int CartId { get; set; }
        public int RestaurantId { get; set; }
        public List<int> DeliveryOrdersIds;
        public double Points { get; set; }
        public virtual CustomerType Type { get; set; }
        public int LogicalDeleted { get; set; }
        public int Commented { get; set; }

        public bool Blocked { get; set; }

        public User() : base() { }

        public User(Guid id, String username, String password, String name, String surname, Gender gender,
            String dateOfBirth,
            UserRole role, List<int> customerOrdersIds, int cartId, int restaurantId,
            List<int> deliveryOrdersIds, double points, CustomerType type, int logicalDeleted,
            int commented, bool blocked)
        {
            Id = id;
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
            Commented = commented;
            Blocked = blocked;
        }

        public User(User user)
        {
            Id = user.Id;
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
    }
}
