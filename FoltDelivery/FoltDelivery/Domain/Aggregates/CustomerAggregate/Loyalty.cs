using System;
using FoltDelivery.Core.Enums;
using FoltDelivery.Core.Domain;

namespace FoltDelivery.Domain.Aggregates.CustomerAggregate
{
    public class Loyalty : Entity
    {
        public CustomerTypeName TypeName { get; set; }
        public float Discount { get; set; }
        public int PointsRequired { get; set; }

        public Loyalty(Guid id):base(id) { }
        public Loyalty(Guid id, CustomerTypeName typeName, float discount, int pointsRequired):base(id)
        {
            this.TypeName = typeName;
            this.Discount = discount;
            this.PointsRequired = pointsRequired;
        }

    }

}
