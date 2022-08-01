using System;
using System.Linq;
using System.Collections.Generic;
using FoltDelivery.Infrastructure;
using FoltDelivery.Domain.Aggregates.Product;

namespace FoltDelivery.Domain.Aggregates.Restaurant
{
    public class RestaurantMenu : Entity
    {

        public virtual List<Article> _menuArticle { get; }

        public RestaurantMenu(Guid id):base(id)
        {
            _menuArticle = new List<Article>();
        }

        public RestaurantMenu(IEnumerable<Article> menuArticle) : base(Guid.NewGuid())
        {
            _menuArticle = menuArticle.ToList();
        }


    }
}
