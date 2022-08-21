using System;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Aggregate;

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


        public override void Apply(DomainEvent changes)
        {
            throw new NotImplementedException();
        }
    }
}
