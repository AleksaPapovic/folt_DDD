using System;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.ProductAggregate
{
    public class Product : Entity
    {
        public String Name { get; set; }
        public Double Price { get; set; }
        public ProductType Type { get; set; }
        public Guid RestaurantId { get; set; }
        public int Quantity { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public bool LogicalDeleted { get; set; }

        public Product(Guid id):base(id) { }

        public Product(Guid id, bool logicalDeleted, String name, Double price, ProductType type, Guid restaurantId,
            int quantity, String description, String image) : base(id)
        {
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
