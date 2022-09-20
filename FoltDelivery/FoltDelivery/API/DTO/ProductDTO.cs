using FoltDelivery.Core.Enums;
using System;

namespace FoltDelivery.API.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public MoneyDTO Price { get; set; }
        public ProductType Type { get; set; }
        public Guid RestaurantId { get; set; }
        public int Quantity { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public bool LogicalDeleted { get; set; }
    }
}
