using FoltDelivery.Model.Enums;
using System;

namespace FoltDelivery.Model
{
    public class Product : Entity
    {
        public String Name { get; set; }
        public float Price { get; set; }
        public ArticleType Type { get; set; }
        public int RestaurantId { get; set; }
        public int Quantity { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public int LogicalDeleted { get; set; }

        public Product() { }

        public Product(Guid id, int logicalDeleted, String name, float price, ArticleType type, int restaurantId,
            int quantity, String description, String image)
        {
            Id = id;
            Name = name;
            Price = price;
            Type = type;
            RestaurantId = restaurantId;
            Quantity = quantity;
            Description = description;
            Image = image;
            LogicalDeleted = logicalDeleted;
        }
    }
}
