using System;

namespace FoltDelivery.Model
{
    public class OrderItem : Entity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderItem() { }

        public OrderItem(Guid orderItemId, int productId, int quantity)
        {
            Id = orderItemId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
