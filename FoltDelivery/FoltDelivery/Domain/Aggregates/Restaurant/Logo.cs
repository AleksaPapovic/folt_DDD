using System;
using FoltDelivery.Infrastructure;

namespace FoltDelivery.Domain.Aggregates.Restaurant
{
    public class Logo : Entity
    {
        public String Image { get; set; }

        public Logo(Guid id):base(id) { }

        public Logo(Guid id, String image) : base(id)
        {
            Image = image;
        }
    }
}
