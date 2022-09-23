using System;
using FoltDelivery.Core.Domain;

namespace FoltDelivery.Domain.Aggregates.RestaurantAggregate
{
    public class Logo
    {
        public String Image { get; set; }

        public Logo(){ }

        public Logo(String image)
        {
            Image = image;
        }
    }
}
