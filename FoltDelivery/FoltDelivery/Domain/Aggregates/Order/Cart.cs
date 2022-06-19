using System;

namespace FoltDelivery.Model
{
    public class Cart : Entity
    {
        public int OrderId { get; set; }
        public String Username { get; set; }
        public float Price { get; set; }
        public int LogicalDeleted { get; set; }

        public Cart() { }

        public Cart(Guid id, int orderId, String username, float price,
            int logicalDeleted) 
        {
            Id = id;
            OrderId = orderId;
            Username = username;
            Price = price;
            LogicalDeleted = logicalDeleted;
        }

        public Cart(Cart cart)
        {
            Id = cart.Id;
            OrderId = cart.OrderId;
            Username = cart.Username;
            Price = cart.Price;
            LogicalDeleted = cart.LogicalDeleted;
        }

    }
}
