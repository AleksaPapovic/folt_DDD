using System;
using FoltDelivery.Core.Enums;
using FoltDelivery.Core.Domain.Aggregate;
using FoltDelivery.Core.Domain;

namespace FoltDelivery.Domain.Aggregates.ProductAggregate
{
    public class Product : EventSourcedAggregate
    {
        public String Name { get; set; }
        public virtual Money Price { get; set; }
        public ProductType Type { get; set; }
        public Guid RestaurantId { get; set; }
        public int Quantity { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public bool LogicalDeleted { get; set; }

        public Product(Guid id):base(id) { }


        public override void When(DomainEvent changes)
        {
            throw new NotImplementedException();
        }
    }
}
