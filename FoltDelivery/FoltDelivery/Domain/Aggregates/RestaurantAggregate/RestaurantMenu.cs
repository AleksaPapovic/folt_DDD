using System;
using System.Linq;
using System.Collections.Generic;
using FoltDelivery.Infrastructure;
using FoltDelivery.Domain.Aggregates.ProductAggregate;

namespace FoltDelivery.Domain.Aggregates.RestaurantAggregate
{
    public class RestaurantMenu : Entity
    {

        public virtual List<Product> _menuProducts { get; }

        public RestaurantMenu(Guid id):base(id)
        {
            _menuProducts = new List<Product>();
        }

        public RestaurantMenu(IEnumerable<Product> menuProducts) : base(Guid.NewGuid())
        {
            _menuProducts = menuProducts.ToList();
        }


    }
}
