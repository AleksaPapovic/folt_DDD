using System;
using FoltDelivery.Model.Enums;

namespace FoltDelivery.Model
{
    public class CustomerType : Entity
    {
        public CustomerTypeName TypeName { get; set; }
        public float Discount { get; set; }
        public int PointsRequired { get; set; }

        public CustomerType() { }
        public CustomerType(Guid id, CustomerTypeName typeName, float discount, int pointsRequired)
        {
            Id = id;
            this.TypeName = typeName;
            this.Discount = discount;
            this.PointsRequired = pointsRequired;
        }

    }

}
