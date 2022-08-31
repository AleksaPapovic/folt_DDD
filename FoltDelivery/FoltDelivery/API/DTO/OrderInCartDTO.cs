using System;
using System.Collections.Generic;

namespace FoltDelivery.API.DTO
{
    public class OrderInCartDTO
    {
        public Guid OwnerId { get; set; }
        public List<Guid> OrderItemsIds { get; set; }
    }
}
