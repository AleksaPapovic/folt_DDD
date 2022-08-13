using FoltDelivery.Infrastructure;
using System;

namespace FoltDelivery.API.DTO
{
    public class OrderItemsUpdateDTO : Entity
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }

        public OrderItemsUpdateDTO(Guid id) : base(id) { }
    }
}
