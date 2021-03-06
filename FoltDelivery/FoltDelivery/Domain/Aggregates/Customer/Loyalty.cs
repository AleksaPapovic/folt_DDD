using System;
using FoltDelivery.Model.Enums;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.Customer
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
